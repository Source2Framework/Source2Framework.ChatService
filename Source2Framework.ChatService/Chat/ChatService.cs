namespace Source2Framework.Services.Chat
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Plugin;
    using CounterStrikeSharp.API.Modules.Utils;
    using Microsoft.Extensions.Logging;

    using Source2Framework.Extensions;
    using Source2Framework.Models;
    using Source2Framework.Services.Command;

    public sealed partial class ChatService : SharedService<ChatService>, IChatService
    {
        public required ICommandService CommandService;

        public static string ChatPrefix { get; set; } = string.Empty;

        public ChatService(ILogger<ChatService> logger, IPluginContext pluginContext) : base(logger, pluginContext)
            { }

        public override void Initialize(bool hotReload)
        {
            this.Plugin.AddCommandListener("say", this.OnSay);
            this.Plugin.AddCommandListener("say_team", this.OnSayTeam);

            ChatPrefix = ((this.Plugin as ChatServicePlugin)!.Config.ChatPrefix);
        }

        public override void OnServiceInitialized(IService service)
        {
            if (service is ICommandService commandService)
            {
                this.CommandService = commandService;
            }
        }

        public override void Shutdown(bool hotReload)
        {
            this.Plugin.RemoveCommandListener("say", this.OnSay, HookMode.Pre);
            this.Plugin.RemoveCommandListener("say_team", this.OnSayTeam, HookMode.Pre);
        }

        public void PrintToChat(CCSPlayerController player, string message)
        {
            player.CPrintToChat(message);
        }

        public void PrintToChatAll(string message, Func<CCSPlayerController, bool>? predicate = null)
        {
            SDK.CPrintToChatAll(message, predicate);
        }

        public void PrintToTeam(string message, CsTeam team)
        {
            PrintToChatAll(message, (player) => player.Team == team);
        }

        public string GetPrefix()
        {
            return ChatPrefix;
        }

        private bool ShouldIgnore(IEnumerable<string> triggers, string command)
        {
            if (command.Contains(' '))
            {
                command = command.Split(' ').First();
            }

            string? prefix = triggers.FirstOrDefault((t) => command.StartsWith(t));

            if (prefix != null)
            {
                command = command.ReplaceFirst(prefix, command.StartsWith($"{prefix}css") ? string.Empty : "css_");

                if (this.CommandService.IsCommandRegistered(command))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
