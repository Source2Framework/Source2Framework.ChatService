﻿namespace Source2Framework.Services.Chat
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Modules.Commands;

    using Source2Framework.Extensions;

    public sealed partial class ChatService : IChatService
    {
        public event IChatService.OnPlayerSayEvent? OnPlayerSayPre;

        public event IChatService.OnPlayerSayEvent? OnPlayerSay;

        public event IChatService.OnPlayerSayEvent? OnPlayerSayPost;

        private HookResult OnSay(CCSPlayerController? player, CommandInfo info)
        {
            if (player == null || !player.IsValid() || info.GetArg(1).Length == 0)
                return HookResult.Continue;

            string? message = info.GetArg(1);

            if (message == null)
            {
                return HookResult.Continue;
            }

            HookResult preResult = this.OnPlayerSayPre?.Invoke(player, info, ref message) ?? HookResult.Continue;

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

            HookResult result = this.OnPlayerSay?.Invoke(player, info, ref message) ?? HookResult.Continue;

            if (result == HookResult.Handled)
            {
                return HookResult.Handled;
            }

            HookResult postResult = this.OnPlayerSayPost?.Invoke(player, info, ref message) ?? HookResult.Continue;

            if (postResult == HookResult.Changed)
            {
                this.PrintToChatAll(message);
            }

            return HookResult.Handled;
        }
    }
}
