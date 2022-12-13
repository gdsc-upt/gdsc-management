FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GdscManagement/GdscManagement.csproj", "GdscManagement/"]
RUN dotnet restore "GdscManagement/GdscManagement.csproj"
COPY . .
WORKDIR "/src/GdscManagement"
RUN dotnet build "GdscManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GdscManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY docker-entrypoint.sh /usr/bin/docker-entrypoint.sh
RUN chmod +x /usr/bin/docker-entrypoint.sh
ENTRYPOINT ["docker-entrypoint.sh"]

# Link image with github repo
LABEL org.opencontainers.image.source=https://github.com/dsc-upt/gdsc-management
