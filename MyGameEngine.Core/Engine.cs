using MyGameEngine.Core.Extensions;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core;

public abstract class Engine
{
    protected Vector2 _screenSize = new Vector2(1200, 800);
    private string _title;
    private Display? _window;
    private Thread? _gameLoopThread;
    private CancellationTokenSource _threadCancellationToken;
    private static List<Sprite2D> _sprites = new List<Sprite2D>();
    private static List<Shape2D> _shapes = new List<Shape2D>();

    public Color BackgroundColor = Color.Black;
    public Vector2 CameraPosition = Vector2.Zero();
    public float CameraAngle = 0f;

    public Engine(Vector2 screenSize, string title)
    {
        _screenSize = screenSize;
        _title = title;
        Initialize();
    }

    public Engine(string title)
    {
        _title = title;
        Initialize();
    }

    public void Initialize()
    {
        _window = new Display();
        _window.Size = new Size((int)_screenSize.X, (int)_screenSize.Y);
        _window.Text = _title;
        _window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        _window.Paint += Renderer;
        _window.KeyDown += KeyDown;
        _window.KeyUp += KeyUp;
        _window.FormClosing += OnClose;

        _threadCancellationToken = new CancellationTokenSource();
        _gameLoopThread = new Thread(GameLoop);
        _gameLoopThread.Start();

        Application.Run(_window);
    }

    private void KeyUp(object? sender, KeyEventArgs e)
    {
        GetKeyUp(e);
    }

    private void KeyDown(object? sender, KeyEventArgs e)
    {
        GetKeyDown(e);
    }

    private void OnClose(object? sender, FormClosingEventArgs e)
    {
        _threadCancellationToken.Cancel();
    }

    public static IEnumerable<BaseDrawable> GetGameObjects()
    {
        return Enumerable.Empty<BaseDrawable>()
            .Union(_sprites)
            .Union(_shapes);
    }

    public static void RegisterShape(BaseDrawable shape)
    {
        if (shape.GetType() == typeof(Shape2D))
            _shapes.Add((Shape2D)shape);

        if (shape.GetType() == typeof(Sprite2D))
            _sprites.Add((Sprite2D)shape);
    }

    public static void UnregisterShape(BaseDrawable shape)
    {
        if (shape.GetType() == typeof(Shape2D))
            _shapes.Remove((Shape2D)shape);

        if (shape.GetType() == typeof(Sprite2D))
            _sprites.Remove((Sprite2D)shape);
    }

    private void GameLoop()
    {
        try
        {
            Log.Info("Game is loading...");
            OnLoad();
            while (true)
            {
                _threadCancellationToken.Token.ThrowIfCancellationRequested();

                try
                {
                    OnDraw();
                    _window?.BeginInvoke((MethodInvoker)delegate { _window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(10);
                }
                catch
                {

                }
            }
        }
        catch
        {
            Log.Info("Game is closing...");
        }
    }



    private void Renderer(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(BackgroundColor);
        g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
        g.RotateTransform(CameraAngle);

        try
        {
            foreach (var shape in _shapes)
            {
                g.FillRectangle(new SolidBrush(shape.Fill), shape.Rectangle());
            }

            foreach (var sprite in _sprites)
            {
                g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }
    }

    public abstract void OnLoad();

    /// <summary>
    /// Before the graphics are drawn
    /// </summary>
    public abstract void OnDraw();

    /// <summary>
    /// After the graphics are drawn
    /// </summary>
    public abstract void OnUpdate();

    public abstract void GetKeyUp(KeyEventArgs e);
    public abstract void GetKeyDown(KeyEventArgs e);
}
