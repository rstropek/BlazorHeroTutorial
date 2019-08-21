# Tour of Heroes - Blazor Demo

This project aims to give some insights into how both server side (with signal r) and client side (with web assembly) blazor can be used to write applications.

## Prerequisites

- Install the latest .NET Core 3.0 SDK preview (https://dotnet.microsoft.com/download/dotnet-core/3.0)
- Clone the repository

## Run the demo

- Run the following commands:

```bash
dotnet build HeroesModel
dotnet build HeroesApi
dotnet build HeroesRazorLib
dotnet build HeroesServer
dotnet build HeroesWasm

dotnet run -p HeroesApi

# Either one of the following two:
dotnet run -p HeroesServer
dotnet run -p HeroesWasm
```

## Replay demo

- Follow the instructions in storyboard.md

This project is based on the Angular "Tour of Heroes" tutorial (https://angular.io/tutorial). 

The code and its licence can be found at: https://github.com/angular/angular/blob/master/aio/content/license.md
