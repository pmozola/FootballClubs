FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FootballClubs.API/FootballClubs.API.csproj", "FootballClubs.API/"]
COPY ["auth/FootballClubs.Auth/FootballClubs.Auth.csproj", "auth/FootballClubs.Auth/"]
RUN dotnet restore "FootballClubs.API/FootballClubs.API.csproj"
COPY . .
WORKDIR "/src/FootballClubs.API"
RUN dotnet build "FootballClubs.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FootballClubs.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FootballClubs.API.dll"]
