# Blazor Heroes Storyboard

**Theme:** Manage your Heroes

## Prerequisites

- Install the latest .NET Core 3.0 SDK preview (https://dotnet.microsoft.com/download/dotnet-core/3.0)
- Install the Blazor templates:

```powershell
dotnet new -i Microsoft.AspNetCore.Blazor.Templates::3.0.0-preview8.19405.7
```
- More information can be found at https://docs.microsoft.com/en-gb/aspnet/core/blazor/get-started?view=aspnetcore-3.0&tabs=netcore-cli

## The Application Shell and setting things up - Step 0

### What you will learn in this chapter:

- Create a reusable Razor Class Lib for our application code
- Create a Blazor Server Side project that takes advantage of the class lib and communicates with the browser via SignalR
- Create a Blazor Client Side project that takes advantage of the class lib and runs using web assembly



* Run the following `dotnet` commands to generate the needed `.NET Core` projects:

```bash
# The app functionality in a Razor Class Library:
dotnet new razorclasslib -o HeroesRazorLib
# Server side version that communicates via SignalR:
dotnet new blazorserver -o HeroesServer
# Client side version that runs on Web Assembly (An ASP.NET Core server hosts the files):
dotnet new blazorwasm --hosted -o HeroesWasm
```

* Navigate to */HeroesRazorLib* and delete the following auto-generated files:
    * *Component1.razor*
    * *ExampleJsInterop.cs*
    * */wwwroot/background.png*
    * */wwwroot/exampleJsInterop.js*

* Create directory */Pages*

* Copy ...
    * ... *0/RazorLib/Pages/Index.razor* into */Pages*
    * ... *0/RazorLib/wwwroot/css/style.css* into */wwwroot/css/*
    * ... *0/RazorLib/App.razor* and *0/RazorLib/_Imports.razor* into *RazorLib/* root

* Navigate to */HeroesServer* and delete the following auto-generated directories:
    * */Pages* 
    * */Data* 
    * */Shared* 
    * */wwwroot* 
    * *_Imports.razor* file
    * *App.razor* file

* Create directory */Blazor* and copy file *0/Server/Blazor/_Host.cshtml* into it

* Replace *Startup.cs* file with */0/Server/Startup.cs*

* Add reference for class lib in *HeroesServer.csproj*:

```xml
<ItemGroup>
    <ProjectReference Include="..\HeroesRazorLib\HeroesRazorLib.csproj" />
</ItemGroup>
```

* Navigate to *HeroesWasm/* and delete the following:
    * *HeroesWasm.sln*
    * */Shared*
    * */Server*
    * */Client/Pages*
    * */Client/Shared*
    * */Client/wwwroot/css*
    * *_Imports.razor*
    * *App.razor*

* Copy *0/Wasm/wwwroot/index.html* into according directory

* Replace the Configure Method's content in *Startup.cs* with this line:
```cs
app.AddComponent<HeroesRazorLib.App>("app");
```

## The Hero Editor - Step 1

### What you will learn in this chapter:

- Import the Hero Model
- Create a component to display our Heroes
- Use Data binding to alter the hero's name



* Insert the .Net Standard project *HeroesModel/* and add a reference for it in the HeroesRazorLib.csproj:

```xml
<ItemGroup>
    <ProjectReference Include="..\HeroesModel\HeroesModel.csproj" />
</ItemGroup>
```

- Also add `@using HeroesModel` to *_Imports.razor* in HeroesRazorLib's root.

- Add `<Heroes />` to *RazorLib/Pages/Index.razor* file after the title
- Copy ...
    - ... *1/RazorLib/Pages/Heroes.razor* into */Pages*

## Displaying a List - Step 2

### What you will learn in this chapter:

- Create a `service` for our data
- How to use a `for` loop and an `if` statement in html markup
- How to bind to a `click` event



* Copy...
  * ... Replace *2/RazorLib/Pages/Heroes.razor*
  * ... Copy *2/RazorLib/Data/MockHeroes.cs* into RazorLib root

* Add `@using HeroesCore` to *HeroesRazorLib/_Imports.razor*

## Master Detail Components - Step 3

### What you will learn in this chapter:

- How to create another component for our selected hero and pass data to it



* Copy...
  * ... *3/RazorLib/Pages/Heroes.razor* into */HeroesRazorLib/Pages*
  * ... *3/RazorLib/Pages/HeroDetail.razor* into */HeroesRazorLib/Pages*

## Services - Step 4

### What you will learn in this chapter:

- Create Services for retrieving heroes and sending notifications when the former happens
- How to inject them into components



* Add the following code in *RazorLib/Index.razor* html:
```html
@if (MessageService.Messages != null) 
{
    <div class="messages">
        <h2>Messages</h2>
        <button class="clear" @onclick="MessageService.Clear">clear</button>
        @foreach(var message in @MessageService.Messages)
        {
            <div>@message</div>
        }
    </div>
}
```

- Copy the content of *4/RazorLib/Data* and *4/RazorLib/wwwroot* and *4/RazorLib/Pages* to the according folders.

- Provide the services in the `ConfigureServices` method in both server and wasm projects in the `Startup.cs` files

```cs
services.AddSingleton<IMessageService, MessageService>();
services.AddSingleton<IHeroService, HeroService>();
```

## Routing - Step 5

### What you will learn in this chapter:

- How to use the `@layout` concept
- How to use routing

* Copy the contents of the following folders to the according places:
    * *5/RazorLib/Pages*
    * *5/RazorLib/Shared*
    * *5/RazorLib/wwwroot*

* In *HeroService.sc* and *IHeroService.cs* add the method:

```cs
        public Hero GetHero(int id)
        {
            var hero = MockHeroes.Heroes.Find(h => h.Id == id);
            _messageService.Add($"HeroService: fetched hero #{hero.Id}");
            return hero;
        }
````

## HTTP - Step 6

### What you will learn in this chapter:
- How to use the http client to make REST request to our ASP.Net Core Web API
- Add functionality for adding as well as deleting heroes.

* Add
```xml
    <PackageReference Include="System.Text.Json" Version="4.6.0-preview8.19405.3" />
````
to HeroesWasm.proj and

```xml
    <PackageReference Include="Microsoft.AspNetCore.Blazor.HttpClient" Version="3.0.0-preview8.19405.7" />
    <PackageReference Include="System.Text.Json" Version="4.6.0-preview8.19405.3" />
```

to HeroesRazorLib.proj.

* Insert the .Net Core project *HeroesApi/* . That's the REST client for our http requests.

Add

```cs
services.AddSingleton((_) => new HttpClient() { BaseAddress = new Uri("http://localhost:61412/", UriKind.Absolute) });
```

to the ConfigureServices method in *HeroesServer/Startup.cs*

- In *HeroesRazorLib/*

    - Replace the *Data/* and *Pages/* folders as well as *wwwroot/* with the matching folders in *6/RazorLib*

    - Add `@using System.Net.Http` to *_Imports.razor*

That's it, you've made it!

> __Tip:__ An extensive collection of blazor resources can be found in this github repo: https://github.com/AdrienTorris/awesome-blazor.
