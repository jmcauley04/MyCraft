using MyGameEngine.Shared.Interfaces;

namespace MyGameEngine.Core;

public static class ServiceProvider
{
    static IGameManager? _gameManager;
    static IViewManager? _viewManager;

    public static IGameManager GameManager => _gameManager ??= new GameManager();
    public static IViewManager ViewManager => _viewManager ??= new ViewManager();
}
