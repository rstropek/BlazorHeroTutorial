# Blazor Heroes Storyboard

**Theme:** Manage your Heroes

## The Application Shell and setting things up - Step 0

* Run the following `dotnet` commands to generate the needed `.NET Core` projects:

```bash
# The app functionality in a Razor Class Library:
dotnet new razorclasslib -o HeroesRazorLib
# Server side version that communicates via SignalR:
dotnet new blazorserver -o HeroesServer
# Client side version that runs on Web Assembly (An ASP.NET Core server hosts the files):
dotnet new blazorwasm --hosted -o HeroesWasm
# REST API to showcase HTTP functionality:
dotnet new webapi -o HeroesApi
```

* Navigate to */HeroesRazorLib* and delete the following auto-generated files:
    * *Component1.razor*
    * *ExampleJsInterop.cs*
    * */wwwroot/background.png*
    * */wwwroot/exampleJsInterop.js*

* Create directory */Pages*

* Copy ...
    * ... *0/Index.razor* into */Pages*
    * ... *0/site.css* into */wwwroot/css/*
    * ... *0/App.razor* and *0/_Index.razor* into */*

* Navigate to */HeroesServer* and delete the following auto-generated directories:
    * */Pages* 
    * */Data* 
    * */Shared* 
    * */wwwroot* 
    * *_Imports.razor* file
    * *App.razor* file

* Create directory */Blazor* and copy file *_Host.cshtml* into it

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

* Add `<Heroes />` to Pages/Index.razor file after the title
* Copy ...
    * ... *1/RazorLib/Pages/Heroes.razor* into */Pages*
    * ... *1/RazorLib/Model/Hero.cs* into */Model*

## Displaying a List - Step 2

* Copy...
  * ... Replace *2/RazorLib/Pages/Heroes.razor*
  * ... Copy *2/RazorLib/Data/MockHeroes.cs* into RazorLib root

* Add `@using HeroesCore` to *HeroesRazorLib/_Imports.razor*

## Master Detail Components - Step 3

* Copy...
  * ... *3/RazorLib/Pages/Heroes.razor* into */HeroesRazorLib/Pages*
  * ... *3/RazorLib/Pages/HeroDetail.razor* into */HeroesRazorLib/Pages*

## Services - Step 4

* Add following code in *Index.razor* html:
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

* Copy the content of *4/RazorLib/Data* and *4/RazorLib/wwwroot* to the according folders.

## Routing - Step 5

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
````

to the ConfigureServices method in *HeroesServer/Startup.cs*

* In *HeroesRazorLib/*

    * Replace the *Data/* and *Pages/* folders as well as the *wwwroot/*

    * Add `@using System.Net.Http` to *_Imports.razor*

