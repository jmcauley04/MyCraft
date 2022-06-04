using MyGameEngine.Core;

namespace MyCraft.UI
{
    public class HealthBar : ResourceBar
    {
        public override string PrimaryColor => "#1EBD07";
        public override string GradientColor => "#1E9D07";

        protected override void InitializeValues()
        {
            var player = ServiceProvider.GameManager.GetPlayer();
            ResourceValue = player.Health;
            MaxResourceValue = player.Health;
        }

        protected override void SetResourcePool()
        {
            var player = ServiceProvider.GameManager.GetPlayer();
            _resourcePool = player.HealthPool;
        }
    }
}
