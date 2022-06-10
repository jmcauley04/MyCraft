namespace MyGameEngine.Core.Models;

public class Player : Sprite2D
{
    public Action OnDeath;

    public int Health { get; internal set; }
    public int MaxHealth { get; internal set; }
    public bool IsAlive { get; internal set; }

    public Player()
    {
        IsAlive = true;
        MaxHealth = Health = 100;

        OnDeath += () => Log.Info("Oh no! I'm dead, blehh!");
    }

    public void Die()
    {
        IsAlive = false;
        OnDeath?.Invoke();
    }
}
