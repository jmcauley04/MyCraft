using MyGameEngine.Core.UI;
using MyGameEngine.Shared.Interfaces;
using System.Diagnostics;

namespace MyGameEngine.Core
{
    public class Player : IPlayer
    {
        public double Health { get; private set; } = 100;
        public double Mana { get; private set; } = 30;
        public Action<double>? OnDamageTaken { get; set; }
        public Action? OnDeath { get; set; }
        public IResourcePool HealthPool { get; set; }
        public IResourcePool ManaPool { get; set; }

        public Player()
        {
            HealthPool = new ResourceBar();
            ManaPool = new ResourceBar();

            HealthPool.SetResource(Health);
            HealthPool.SetMaxResource(Health);

            ManaPool.SetResource(Mana);
            ManaPool.SetMaxResource(Mana);
        }

        void Die()
        {
            Debug.WriteLine("Oh no I died - blehh!");
            OnDeath?.Invoke();
        }

        public void TakeDamage(double damage)
        {
            Health = Math.Max(0, Health - damage);
            HealthPool.SetResource(Health);

            if (Health <= 0)
                Die();
        }
    }
}
