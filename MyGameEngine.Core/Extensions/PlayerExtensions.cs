using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Extensions;

public static class PlayerExtensions
{
    public static float PercentHealth(this Player player) => player.Health * 1f / player.MaxHealth;

    public static void ModifyHealth(this Player player, int change)
    {
        if (change < 0)
            player.Health = Math.Max(0, player.Health + change);

        if (change > 0)
            player.Health = Math.Min(player.MaxHealth, player.Health + change);

        if (player.Health <= 0)
            player.Die();
    }
}
