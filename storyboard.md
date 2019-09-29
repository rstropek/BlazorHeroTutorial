# Blazor Heroes Storyboard

**Theme:** Manage your Heroes

## Prerequisites

Follow the [installation guide in the Blazor docs](https://docs.microsoft.com/en-us/aspnet/core/blazor/get-started) to get the prerequisites of this hands-on-lab.

## The Application Shell and setting things up - Step 0

### What you will learn in this chapter

- Create a reusable [Razor Class Lib](https://docs.microsoft.com/en-us/aspnet/core/blazor/class-libraries) for our application code
- Create a [Blazor Server Side](https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models#blazor-server) project that takes advantage of the class lib and communicates with the browser via SignalR
- Create a [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models#blazor-webassembly) project that takes advantage of the class lib and runs using web assembly

### Creating Solution and Projects

* Run the following `dotnet` commands to generate the needed `.NET Core` projects:

```bash
# The app functionality in a Razor Class Library:
dotnet new razorclasslib -o HeroesRazorLib
# Server side version that communicates via SignalR:
dotnet new blazorserver -o HeroesServer
# Client side version that runs on Web Assembly (An ASP.NET Core server hosts the files):
dotnet new blazorwasm -o HeroesWasm
# Create solution file and add projects
dotnet new sln -n BlazorHeroTutorial
dotnet sln BlazorHeroTutorial.sln add HeroesRazorLib
dotnet sln BlazorHeroTutorial.sln add HeroesServer
dotnet sln BlazorHeroTutorial.sln add HeroesWasm
# Add reference from server to class library
cd HeroesServer
dotnet add reference ..\HeroesRazorLib\HeroesRazorLib.csproj
cd ..
cd HeroesWasm
dotnet add reference ..\HeroesRazorLib\HeroesRazorLib.csproj
```

* Open the generated solution in Visual Studio

### Build Basic App Structure

* Navigate to */HeroesRazorLib* and delete the following auto-generated files. We don't need them in this lab.
  * *Component1.razor*
  * *ExampleJsInterop.cs*
  * *wwwroot/background.png*
  * *wwwroot/exampleJsInterop.js*
  * *wwwroot/styles.css*

* Create directory */Pages*

* Inside */wwwroot* create a folder */css*

* Copy ...
  * ... *0/RazorLib/Pages/Index.razor* into */Pages*
  * ... *0/RazorLib/wwwroot/css/style.css* into */wwwroot/css/*
  * ... *0/RazorLib/App.razor* and *0/RazorLib/_Imports.razor* into *RazorLib/* root
  * ... *0/RazorLib/Shared/HeroesLayout.razor* into */Shared*

* Make yourself familiar with the content of the files you copied. Note especially:
  * Data binding in *Index.razor*
  * [*Router* component](https://docs.microsoft.com/en-us/aspnet/core/blazor/routing#route-templates) in *App.razor*
  * *using* statements for pages in *_Imports.razor*
  * [Layouts](https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts)

* Navigate to */HeroesServer* and delete the following auto-generated directories:
  * */Pages* 
  * */Data* 
  * */Shared* 
  * */wwwroot* 
  * *_Imports.razor* file
  * *App.razor* file

* Create directory */Blazor* and copy file *0/Server/Blazor/_Host.cshtml* into it

* Replace *Startup.cs* file with */0/Server/Startup.cs*

* Make yourself familiar with the content of the files you copied. Note especially:
  * Bootstrapping of Blazor in *_Host.cshtml* with [`Html.RenderComponentAsync`](https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models#stateful-reconnection-after-prerendering)
  * Adding Blazor services and endpoints in *Startup.cs*

* We are taking advantage of the C# 8.0 preview. One of the most important changes is support for [nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/nullable-reference-types). Add the following to the `<Property Group />` in all three projects:

```xml
  <PropertyGroup>
    ...
    <Nullable>enable</Nullable>
  </PropertyGroup>
```

* Make sure that `LangVersion` is set to `preview` in .NET Standard 2.0 projects (class library, WASM project):

```xml
  <PropertyGroup>
    ...
    <LangVersion>preview</LangVersion>
  </PropertyGroup>
```

* Navigate to *HeroesWasm/* and delete the following:
    * */Pages*
    * */Shared*
    * */wwwroot/css*
    * */wwwroot/sample-data*
    * *_Imports.razor*
    * *App.razor*

* Copy *0/Wasm/wwwroot/index.html* into corresponding directory of the WASM project

* Replace the Configure Method's content in *Startup.cs* with this line:

```cs
app.AddComponent<HeroesRazorLib.App>("app");
```

Try to compile your solution. You should be able to compile it without any errors.

## The Hero Editor - Step 1

### What you will learn in this chapter

* Import the Hero Model

* Create a component to display our Heroes

* Use Data binding to alter the hero's name

### Adding the Model Project

* Insert the .Net Standard project [*HeroesModel*](https://github.com/rstropek/BlazorHeroTutorial/tree/master/HeroesModel) to your solution and add a reference for it in *HeroesRazorLib.csproj*:

```xml
<ItemGroup>
    <ProjectReference Include="..\HeroesModel\HeroesModel.csproj" />
</ItemGroup>
```

### Adding the First Razor File

* Add `@using HeroesModel` to *_Imports.razor* in HeroesRazorLib's root.

* Copy ...
  * ... *1/RazorLib/Pages/Heroes.razor* into */Pages*

* Make yourself familiar with the content of the file you copied. Note especially:
  * Syntax for [Code Rendering](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor#razor-syntax) with Razor
  * [Data Binding](https://docs.microsoft.com/en-us/aspnet/core/blazor/components#data-binding) with `@bind`

* Add new component using [HTML element syntax](https://docs.microsoft.com/en-us/aspnet/core/blazor/components#use-components) `<Heroes />` to *RazorLib/Pages/Index.razor* instead of the title.

```html
@page "/"

<Heroes />

@code {
}
```

## Displaying a List - Step 2

### What you will learn in this chapter:

* Create a *service* for our data

* How to use a `for` loop and an `if` statement in Razor syntax

* How to bind to a `click` event

### Add Heroes List

* Replace *2/RazorLib/Pages/Heroes.razor*

* Copy *2/RazorLib/Data/MockHeroes.cs* into RazorLib root

* Add `@using HeroesCore` to *HeroesRazorLib/_Imports.razor*

* Make yourself familiar with the content of the file you copied. Note especially:
  * Syntax for [Explicit Razor Expressions](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor#explicit-razor-expressions)
  * Razor [Control Structures](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor#explicit-razor-expressions)
  * Blazor [Event Handling](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor#explicit-razor-expressions)
  * [Lifecycle Method](https://docs.microsoft.com/en-us/aspnet/core/blazor/components#lifecycle-methods) `OnInitialized`

## Master Detail Components - Step 3

### What you will learn in this chapter

* How to create another component for our selected hero and pass data to it

### Add Details Page

* Copy...
  * ... *3/RazorLib/Pages/Heroes.razor* into */HeroesRazorLib/Pages*
  * ... *3/RazorLib/Pages/HeroDetail.razor* into */HeroesRazorLib/Pages*

* Make yourself familiar with the content of the file you copied. Note especially:
  * [Component Parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components#component-parameters)

## Services - Step 4

### What you will learn in this chapter

* Create Services for retrieving heroes and sending notifications when the former happens

* How to inject services into components

### Add Messages to Index

* Copy the content of *4/RazorLib/Data* and *4/RazorLib/wwwroot* and *4/RazorLib/Pages* to the according folders.

* Make yourself familiar with the content of the file you copied. Note especially:
  * Implementation of `INotifyPropertyChanged` in `IMessageService`
  * Use of `InvokeAsync` and `StateHasChanged` to [manually update UI](https://docs.microsoft.com/en-us/aspnet/core/blazor/components#invoke-component-methods-externally-to-update-state) in *Index.razor*
  * [Implementation of `IDisposable`](https://docs.microsoft.com/en-us/aspnet/core/blazor/components?view=aspnetcore-3.0#component-disposal-with-idisposable) in *Index.razor*
  * [Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/blazor/dependency-injection?view=aspnetcore-3.0) in e.g. *Heroes.razor*

* Provide the services in the `ConfigureServices` method in both server and WASM projects in the *Startup.cs* files:

```cs
services.AddSingleton<IMessageService, MessageService>();
services.AddSingleton<IHeroService, HeroService>();
```

## Routing - Step 5

### What you will learn in this chapter

* How to use the `@layout` concept

* How to use routing

### Add Routing and Layout

* Create a folder named *Shared/* in *HeroesRazorLib/* if it doesn't exist already

* Delete *Index.razor* in *Pages/*. We don't need an Index component as we now use a [Blazor layout](https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts).

* Copy the contents of the following folders to the according places:
    * *5/RazorLib/Pages*
    * *5/RazorLib/Shared*
    * *5/RazorLib/wwwroot*

* In *HeroService.cs* and *IHeroService.cs* add the method:

```cs
public Hero GetHero(int id)
{
    var hero = MockHeroes.Heroes.Find(h => h.Id == id);
    _messageService.Add($"HeroService: fetched hero #{hero.Id}");
    return hero;
}
```

* Make yourself familiar with the content of the file you copied. Note especially:
  * Use of `@page` statement for [Routing](https://docs.microsoft.com/en-us/aspnet/core/blazor/routing#route-templates)
  * Use of [`NavigationManager`](https://docs.microsoft.com/en-us/aspnet/core/blazor/routing#uri-and-navigation-state-helpers) to trigger navigation in code
  * Consuming [Route Parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/routing#uri-and-navigation-state-helpers)
  * Use of [Layouts](https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts)

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

## Appendix - Deploy to Docker

Replace the _baseAddress assignment in the HeroService constructor in HeroesRazorLib with the following code:

```cs
// Sets api address based on execution environment
#if DOCKER
_baseAddress = "http://webapi/api/Heroes";
#else
_baseAddress = "http://localhost:8000/api/Heroes";
#endif
``` 

Run the following commands in project root:

```bash
docker-compose build
docker-compose up --remove-orphans
```