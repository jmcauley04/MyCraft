using MyGameEngine.Core.Interfaces;
using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core;

public abstract class Engine
{
    ControllersManager _controllersManager;

    protected Vector2 _screenSize = new Vector2(1200, 800);
    private string _title;
    private Display? _window;
    private Thread? _gameLoopThread;
    private CancellationTokenSource _threadCancellationToken;

    public Color BackgroundColor = Color.Black;

    // TODO: Move Camera properties into something else - CameraManager or Camera object?
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
        _controllersManager = new();
        _window = new();
        _window.Size = new Size((int)_screenSize.X, (int)_screenSize.Y);
        _window.Text = _title;
        _window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        _window.Paint += Renderer;
        _window.KeyDown += KeyDown;
        _window.KeyUp += KeyUp;
        _window.FormClosing += OnClose;
        _window.MouseDown += MouseDown;
        _window.MouseUp += MouseUp;

        _threadCancellationToken = new CancellationTokenSource();
        _gameLoopThread = new Thread(GameLoop);
        _gameLoopThread.Start();

        Application.Run(_window);
    }

    private void OnClose(object? sender, FormClosingEventArgs e)
    {
        _threadCancellationToken.Cancel();
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

    protected T LoadController<T>() where T : IGameController, new()
    {
        return _controllersManager.LoadController<T>();
    }

    private void Renderer(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(BackgroundColor);

        if (CameraManager.ActiveCamera is not null)
        {
            CameraManager.UpdateActiveCamera();
            g.TranslateTransform(-CameraManager.ActiveCamera.Position.X, -CameraManager.ActiveCamera.Position.Y);
            g.RotateTransform(CameraManager.ActiveCamera.Angle);
        }

        try
        {
            foreach (var shape in GameObjectManager.GetGameObjects())
                shape.Draw(g);
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
    public virtual void OnUpdate()
    {
        _controllersManager.Update();
    }

    private void KeyUp(object? sender, KeyEventArgs e)
    {
        GetKeyUp(e);
    }

    private void KeyDown(object? sender, KeyEventArgs e)
    {
        GetKeyDown(e);
    }

    public virtual void GetKeyDown(KeyEventArgs e)
    {
        GetKeys(e, true);
    }

    public virtual void GetKeyUp(KeyEventArgs e)
    {
        GetKeys(e, false);
    }

    private void GetKeys(KeyEventArgs e, bool target)
    {
        _controllersManager.SetKey(e.KeyCode.ToString(), target);
    }

    private void MouseDown(object? sender, MouseEventArgs e)
    {
        SetMouse(e, true);
    }

    private void MouseUp(object? sender, MouseEventArgs e)
    {
        SetMouse(e, false);
    }

    private void SetMouse(MouseEventArgs e, bool target)
    {
        var clickPosition = new Vector2(e.X, e.Y);

        if (CameraManager.ActiveCamera is not null)
            clickPosition += CameraManager.ActiveCamera.Position;

        _controllersManager.SetMouse(e.Button.ToString(), target, clickPosition);
    }
}
