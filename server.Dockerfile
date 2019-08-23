FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet build HeroesServer/*.csproj -p:DefineConstants=DOCKER
RUN dotnet publish HeroesServer/*.csproj -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HeroesServer.dll"]

