namespace Source2Framework
{
    using CounterStrikeSharp.API.Core;

    public sealed partial class PluginConfig : BasePluginConfig
    {
        public string ChatPrefix { get; set; } = "{default}「{lightblue}S2F{default}」{lime} »{default}";
    }
}
