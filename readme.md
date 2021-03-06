# Tour of Heroes - Blazor Demo

This project aims to give some insights into how both server side (with signal r) and client side (with web assembly) blazor can be used to write applications.

## Prerequisites

- Install the latest .NET Core 3.0 SDK preview (https://dotnet.microsoft.com/download/dotnet-core/3.0)
- Clone the repository

## Run the demo

- Open Solution file
- Run HeroesApi plus either HeroesServer or HeroesWasm

## Replay demo

- Follow the instructions in storyboard.md

## Docker

- You can also deploy the application to Docker using the following commands:

```bash
docker-compose build
docker-compose up --remove-orphans
```

- For some details about Docker usage have a look at the appendix in storyboard.md

## Licence

This project is based on the Angular "Tour of Heroes" tutorial (https://angular.io/tutorial). 

The code and its licence can be found at: https://github.com/angular/angular/blob/master/aio/content/license.md
