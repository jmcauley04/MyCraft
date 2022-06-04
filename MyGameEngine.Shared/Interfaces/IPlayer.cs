namespace MyGameEngine.Shared.Interfaces;

public interface IPlayer
{
    double Health { get; }
    double Mana { get; }
    Action<double>? OnDamageTaken { get; set; }
    Action? OnDeath { get; set; }
    IResourcePool HealthPool { get; set; }
    IResourcePool ManaPool { get; set; }
    void TakeDamage(double damage);
}
