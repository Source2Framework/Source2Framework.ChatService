namespace Source2Framework
{
    using CounterStrikeSharp.API.Core;

    using Source2Framework.Services.Chat;

    public class PluginServices : IPluginServiceCollection<ChatServicePlugin>
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ChatService>();
        }
    }
}
