FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY ./HeroesModel ./HeroesModel
COPY ./HeroesRazorLib ./HeroesRazorLib
COPY ./HeroesWasm ./HeroesWasm

RUN dotnet publish HeroesWasm/*.csproj

FROM nginx:alpine
COPY --from=build-env app/HeroesWasm/bin/Debug/netstandard2.0/publish/HeroesWasm.Client/dist /usr/share/nginx/html/
# Copy Stylesheets manually to NGINX directory.
# This is needed because Client Side Blazor does not ship static web assets on its own
# See https://github.com/aspnet/AspNetCore/issues/12588
COPY --from=build-env app/HeroesWasm/bin/Debug/netstandard2.0/publish/wwwroot/_content/HeroesRazorLib/css /usr/share/nginx/html/_content/HeroesRazorLib/css/
COPY nginx.conf /etc/nginx/nginx.conf