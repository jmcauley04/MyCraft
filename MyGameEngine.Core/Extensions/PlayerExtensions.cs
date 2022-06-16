using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Extensions;

public static class PlayerExtensions
{
    public static float PercentHealth(this Player player) => player.Health * 1f / player.MaxHealth;
}
