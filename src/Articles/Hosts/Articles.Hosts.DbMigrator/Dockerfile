FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Articles/Hosts/Articles.Hosts.DbMigrator/Articles.Hosts.DbMigrator.csproj", "src/Articles/Hosts/Articles.Hosts.DbMigrator/"]
RUN dotnet restore "src/Articles/Hosts/Articles.Hosts.DbMigrator/Articles.Hosts.DbMigrator.csproj"
COPY . .
WORKDIR "/src/src/Articles/Hosts/Articles.Hosts.DbMigrator"
RUN dotnet build "Articles.Hosts.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Articles.Hosts.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Articles.Hosts.DbMigrator.dll"]
