namespace Source2Framework.Services.Chat
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Modules.Commands;

    using Source2Framework.Extensions;

    public sealed partial class ChatService : IChatService
    {
        public event IChatService.OnPlayerSayTeamEvent? OnPlayerSayTeamPre;

        public event IChatService.OnPlayerSayTeamEvent? OnPlayerSayTeam;

        public event IChatService.OnPlayerSayTeamEvent? OnPlayerSayTeamPost;

        private HookResult OnSayTeam(CCSPlayerController? player, CommandInfo info)
        {
            if (player == null || !player.IsValid() || info.GetArg(1).Length == 0)
                return HookResult.Continue;

            string? message = info.GetArg(1);

            if (message == null)
            {
                return HookResult.Continue;
            }

            HookResult preResult = this.OnPlayerSayTeamPre?.Invoke(player, info, ref message) ?? HookResult.Continue;

            if (preResult != HookResult.Continue)
            {
                return preResult;
            }

            if (this.ShouldIgnore(CoreConfig.PublicChatTrigger, message))
            {
                return HookResult.Continue;
            }

            if (this.ShouldIgnore(CoreConfig.SilentChatTrigger, message))
            {
                return HookResult.Continue;
            }

            HookResult result = this.OnPlayerSayTeam?.Invoke(player, info, ref message) ?? HookResult.Continue;

            if (result == HookResult.Handled)
            {
                return HookResult.Handled;
            }

            HookResult postResult = this.OnPlayerSayTeamPost?.Invoke(player, info, ref message) ?? HookResult.Continue;

            if (postResult < HookResult.Handled)
            {
                this.PrintToTeam(message, player.Team);
            }

            return HookResult.Handled;
        }
    }
}
