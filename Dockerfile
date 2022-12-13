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
ENTRYPOINT ["dotnet", "GdscManagement.dll", "--migrate"]
