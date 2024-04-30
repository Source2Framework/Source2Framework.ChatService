# Source2Framework.ChatService

> [!IMPORTANT]  
> This module requires [Source2Framework.Core](https://github.com/Source2Framework/Source2Framework) and [Source2Framework.CommandService](https://github.com/Source2Framework/Source2Framework.CommandService)

# Installation
- Download the latest build
- Drag & drop the content to your server

# Service API Reference

Available on [NuGet](https://www.nuget.org/packages/Source2Framework.ChatService.API/)
[![NuGet version (Source2Framework.ChatService.API)](https://img.shields.io/nuget/v/Source2Framework.ChatService.API.svg?style=flat-square)](https://www.nuget.org/packages/Source2Framework.ChatService.API/)

```
dotnet add package Source2Framework.ChatService.API --version 1.0.1
```

# Service interface

```csharp
public interface IChatService : ISharedService
{
    public delegate HookResult OnPlayerSayEvent(CCSPlayerController player, CommandInfo info, ref string message);

    public event OnPlayerSayEvent? OnPlayerSayPre;

    public event OnPlayerSayEvent? OnPlayerSay;

    public event OnPlayerSayEvent? OnPlayerSayPost;

    public delegate HookResult OnPlayerSayTeamEvent(CCSPlayerController player, CommandInfo info, ref string message);

    public event OnPlayerSayTeamEvent? OnPlayerSayTeamPre;

    public event OnPlayerSayTeamEvent? OnPlayerSayTeam;

    public event OnPlayerSayTeamEvent? OnPlayerSayTeamPost;

    public string GetPrefix();

    public void PrintToChat(CCSPlayerController player, string message);

    public void PrintToChatAll(string message, Func<CCSPlayerController, bool>? predicate = null);

    public void PrintToTeam(string message, CsTeam team);
}
```
