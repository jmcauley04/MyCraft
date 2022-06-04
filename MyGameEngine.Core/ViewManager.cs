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

        public ViewManager()
        {
            if (_view is null)
                _view = GenerateView();
        }

        public ColorRecord[,,] GetView()
        {
            return _view;
        }

        private ColorRecord[,,] GenerateView()
        {
            var view = new ColorRecord[ResolutionX, ResolutionY, LayersZ];

            for (int x = 0; x < ResolutionX; x++)
            {
                for (int y = 0; y < ResolutionY; y++)
                {
                    if (y > ResolutionY - 20 && y <= ResolutionY - 19)
                        view[x, y, 0] = new ColorRecord(40, 200, 40, x, y);

                    if (y > ResolutionY - 19 && y <= ResolutionY - 0)
                        view[x, y, 0] = new ColorRecord(200, 150, 40, x, y);
                }
            }

            return view;
        }

        public void UpdateView(ColorRecord[,,] view)
        {
            foreach (var color in view)
            {
                _view[color.X, color.Y, color.Z] = color;
            }

            OnViewChanged?.Invoke(view);
        }
    }
}
