using MyGameEngine.Core.GameObjects.Blocks;
using MyGameEngine.Shared;
using MyGameEngine.Shared.Interfaces;
using MyGameEngine.Shared.Records;

namespace MyGameEngine.Core
{
    public class ViewManager : IViewManager
    {
        public int ResolutionX { get; private set; } = 120;
        public int ResolutionY { get; private set; } = 80;
        public int LayersZ { get; private set; } = 1;

        public Action<ColorRecord[,,]>? OnViewChanged { get; set; }

        private ColorRecord[,,] _view;
        List<IRenderableGameObject> _gameObjects = new();

        public ViewManager()
        {
            if (_view is null)
                _view = GenerateView();

            for (int x = 0; x < _view.GetLength(0); x += 3)
                for (int y = 60; y < _view.GetLength(1); y += 3)
                {
                    if (y == 60)
                        PlaceBlockAt<GrassBlock>(x, y);
                    else
                        PlaceBlockAt<DirtBlock>(x, y);
                }
        }

        public ColorRecord[,,] GetView()
        {
            return _view;
        }

        private ColorRecord[,,] GenerateView()
        {
            var view = new ColorRecord[ResolutionX, ResolutionY, LayersZ];

            foreach (var gameObject in _gameObjects)
            {
                foreach (var pixel in gameObject.GetPixels())
                {
                    var x = pixel.X;
                    var y = pixel.Y;
                    var worldPixel = pixel with
                    {
                        X = gameObject.XPosition + x,
                        Y = gameObject.YPosition + y
                    };

                    if (gameObject.XPosition + pixel.X < view.GetLength(0) && gameObject.YPosition + pixel.Y < view.GetLength(1))
                        view[gameObject.XPosition + pixel.X, gameObject.YPosition + pixel.Y, 0] = worldPixel;
                }
            }

            return view;
        }

        public void UpdateView(IBlock block)
        {
            var changedView = new ColorRecord[block.Width, block.Height, 1];

            foreach (var pixel in block.GetPixels())
            {
                var x = pixel.X;
                var y = pixel.Y;
                var worldPixel = pixel with
                {
                    X = block.XPosition + x,
                    Y = block.YPosition + y
                };

                if (block.XPosition + pixel.X < _view.GetLength(0) && block.YPosition + pixel.Y < _view.GetLength(1))
                {
                    _view[block.XPosition + pixel.X, block.YPosition + pixel.Y, 0] = worldPixel;
                    changedView[pixel.X, pixel.Y, 0] = worldPixel with
                    {
                        X = pixel.X + block.XPosition,
                        Y = pixel.Y + block.YPosition
                    };
                }
            }

            OnViewChanged?.Invoke(changedView);
        }

        public void PlaceBlockAt<T>(int posX, int posY) where T : IBlock, new()
        {
            var block = new T();
            block.Place(posX, posY);
            _gameObjects.Add(block);

            UpdateView(block);
        }
    }
}
