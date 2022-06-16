using MyGameEngine.Core.Managers;

namespace MyGameEngine.Core.Models;

public class Player : Sprite2D
{
    public Action OnDeath;

    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }
    public int Layer { get; private set; } = 0;

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

    public void Fall()
    {
        Layer--;
        GameObjectManager.UpdateLayer(this, Layer);
    }

    public void ModifyHealth(int change)
    {
        if (change < 0)
            Health = Math.Max(0, Health + change);

        if (change > 0)
            Health = Math.Min(MaxHealth, Health + change);

        if (Health <= 0)
            Die();
    }
}
