FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish HeroesWasm/*.csproj

FROM nginx:alpine
COPY --from=build-env app/HeroesWasm/bin/Debug/netstandard2.0/publish/HeroesWasm.Client/dist /usr/share/nginx/html/
COPY nginx.conf /etc/nginx/nginx.conf