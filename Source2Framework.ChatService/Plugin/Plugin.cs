namespace Source2Framework
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Attributes;

    using Source2Framework.Models;

    using Source2Framework.Services.Chat;

    [MinimumApiVersion(213)]
    public sealed partial class ChatServicePlugin : BasePlugin, IPluginConfig<PluginConfig>, IS2FModule
    {
        public required IFramework Framework { get; set; }

        public required PluginConfig Config { get; set; } = new PluginConfig();

        public readonly ChatService ChatService;

        public ChatServicePlugin
            (
                ChatService chatService
            )
        {
            this.ChatService = chatService;
        }

        public void OnConfigParsed(PluginConfig config)
        {
            this.Config = config;
        }

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            using (ModuleLoader loader = new ModuleLoader())
            {
                loader.Attach(this, hotReload);
            }
        }

        public void OnCoreReady(IFramework framework, bool hotReload)
        {
            (this.Framework = framework).Services.RegisterService<ChatService>(this.ChatService, true);
        }

        public override void Unload(bool hotReload)
        {
            this.Framework?.Services.RemoveService<ChatService>();
        }
    }
}
