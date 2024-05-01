FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi.Redis.Api/WebApi.Redis.Api.csproj", "WebApi.Redis.Api/"]
COPY ["WebApi.Redis.Cache/WebApi.Redis.Cache.csproj", "WebApi.Redis.Cache/"]
RUN dotnet restore "./WebApi.Redis.Api/WebApi.Redis.Api.csproj"
COPY . .
WORKDIR "/src/WebApi.Redis.Api"
RUN dotnet build "./WebApi.Redis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.Redis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.Redis.Api.dll"]
