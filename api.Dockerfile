FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY ./HeroesModel ./HeroesModel
COPY ./HeroesApi ./HeroesApi

RUN dotnet publish HeroesApi/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HeroesApi.dll"]

