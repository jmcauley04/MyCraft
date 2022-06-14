using MyGameEngine.Core.Controllers;
using MyGameEngine.Core.Models;

namespace MyCraft2D.Controllers
{
    public class StatusBarsController : GameController
    {
        Player? _player;
        Shape2D? _bgBar;
        Shape2D? _healthbar;
        Shape2D? _manabar;
        Shape2D? _staminabar;

        int maxWidth = 80;
        int barHeight = 10;

        public StatusBarsController()
        {
            //var padding = 1;
            //var init = new Vector2(1000, 10);

            //_bgBar = GameObjectFactory.RegisterShape<Shape2D>(
            //    init,
            //    new Vector2(maxWidth + padding * 2, barHeight + padding * 2),
            //    "ui_bar");
            //_bgBar.Fill = Color.WhiteSmoke;

            //_healthbar = GameObjectFactory.RegisterShape<Shape2D>(
            //    new Vector2(init.X + padding, init.Y + padding),
            //    new Vector2(maxWidth, barHeight),
            //    "hp_barbg");
            //_healthbar.Fill = Color.DarkRed;

            //_healthbar = GameObjectFactory.RegisterShape<Shape2D>(
            //    new Vector2(init.X + padding, init.Y + padding),
            //    new Vector2(maxWidth, barHeight),
            //    "hp_barfg");
            //_healthbar.Fill = Color.Red;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public override void OnUpdate()
        {
            //_healthbar.Scale.X = (int)(_player.PercentHealth() * maxWidth);
            //if(CameraManager.ActiveCamera is not null && CameraManager.ActiveCamera.Target is not null{
            //    _bgBar.Position.Update()
            //}
        }
    }
}
