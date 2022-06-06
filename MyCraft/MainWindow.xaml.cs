using MyCraft.Editor;
using MyGameEngine.Core;
using MyGameEngine.Shared;
using MyGameEngine.Shared.Records;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyCraft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle[,] _pixels = new Rectangle[0, 0];
        int _viewWidth;
        int _viewHeight;
        private IViewManager _viewManager;

        EditorMenu? _editorMenu;

        public MainWindow()
        {
            InitializeComponent();

            _viewWidth = (int)viewport.Width;
            _viewHeight = (int)viewport.Height;

            _viewManager = ServiceProvider.ViewManager;
            _viewManager.OnViewChanged += UpdateView;
        }

        private void UpdateView(ColorRecord[,,] view)
        {
            foreach (var color in view)
            {
                if (color is not null)
                {
                    var existingRectangle = _pixels[color.X, color.Y];

                    if (existingRectangle is null)
                        _pixels[color.X, color.Y] = GetRectangle(color);
                    else
                        existingRectangle.Fill = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
                }
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            _viewWidth = (int)sizeInfo.NewSize.Width;
            _viewHeight = (int)sizeInfo.NewSize.Height;

            ClearMap();
            _pixels = GetColorMap();
            DrawMap();

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void ClearMap()
        {
            if (_pixels is null)
                return;

            foreach (var rect in _pixels)
                main_canvas.Children.Remove(rect);
        }

        private Rectangle[,] GetColorMap()
        {
            var pixels = new Rectangle[_viewManager.ResolutionX, _viewManager.ResolutionY];
            var view = _viewManager.GetView();

            for (int x = 0; x < view.GetLength(0); x++)
            {
                for (int y = 0; y < view.GetLength(1); y++)
                {
                    for (int z = 0; z < view.GetLength(2); z++)
                    {
                        var color = view[x, y, z];

                        if (color is null)
                            continue;

                        pixels[x, y] = GetRectangle(color);
                    }
                }
            }

            return pixels;
        }

        private Rectangle GetRectangle(ColorRecord color)
        {
            return new Rectangle()
            {
                Width = _viewWidth / _viewManager.ResolutionX,
                Height = _viewHeight / _viewManager.ResolutionY,
                Fill = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B))
            };
        }

        private void DrawMap()
        {
            for (int x = 0; x < _pixels.GetLength(0); x++)
            {
                for (int y = 0; y < _pixels.GetLength(1); y++)
                {
                    var rectangle = _pixels[x, y];
                    if (rectangle is not null)
                    {
                        Canvas.SetTop(rectangle, y * rectangle.Height);
                        Canvas.SetLeft(rectangle, x * rectangle.Width);
                        main_canvas.Children.Add(rectangle);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceProvider.GameManager.GetPlayer().TakeDamage(10);
        }

        private void OpenCanvasContextMenu(object sender, RoutedEventArgs e)
        {
            ContextMenu? cm = main_canvas.FindResource("cmCanvas") as ContextMenu;

            if (cm is not null)
            {
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
            }
        }

        private void OpenEditorMenu(object sender, RoutedEventArgs e)
        {
            if (_editorMenu is null)
            {
                _editorMenu = new EditorMenu();
                _editorMenu.Closed += (s, e) => _editorMenu = null;
            }

            _editorMenu.Show();
            _editorMenu.Activate();
        }
    }
}
