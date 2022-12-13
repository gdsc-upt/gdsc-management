FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["GdscManagement/*.csproj", "GdscManagement/"]
COPY ["GdscManagement.API/*.csproj", "GdscManagement.API/"]
COPY ["GdscManagement.Common/*.csproj", "GdscManagement.Common/"]

RUN dotnet restore

COPY . .
WORKDIR /src/GdscManagement

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY docker-entrypoint.sh /usr/bin/docker-entrypoint.sh
RUN chmod +x /usr/bin/docker-entrypoint.sh
ENTRYPOINT ["docker-entrypoint.sh"]

# Link image with github repo
LABEL org.opencontainers.image.source=https://github.com/dsc-upt/gdsc-management
