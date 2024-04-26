namespace Source2Framework
{
    using CounterStrikeSharp.API.Core;

    public sealed partial class ChatServicePlugin : BasePlugin
    {
        public override string ModuleName => "Source2Framework Chat Service";

        public override string ModuleDescription => "Exposing functionality for chat";

        public override string ModuleAuthor => "Nexd @ Eternar";

        public override string ModuleVersion => "1.0.0 " +
#if RELEASE
            "(release)";
#else
            "(debug)";
#endif
    }
}
