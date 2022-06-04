using MyGameEngine.Core;

namespace MyCraft.UI
{
    public class ManaBar : ResourceBar
    {
        public override string PrimaryColor => "#318ce7";
        public override string GradientColor => "#1566b6";

        protected override void InitializeValues()
        {
            var player = ServiceProvider.GameManager.GetPlayer();
            ResourceValue = player.Mana;
            MaxResourceValue = player.Mana;
        }

        protected override void SetResourcePool()
        {
            var player = ServiceProvider.GameManager.GetPlayer();
            _resourcePool = player.ManaPool;
        }
    }
}
