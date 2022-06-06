using MyGameEngine.Shared;
using MyGameEngine.Shared.Interfaces;

namespace MyGameEngine.Core;

public static class ServiceProvider
{
    static IGameManager? _gameManager;
    static IViewManager? _viewManager;
    static IEditManager? _editManager;

    public static IGameManager GameManager => _gameManager ??= new GameManager();
    public static IViewManager ViewManager => _viewManager ??= new ViewManager();
    public static IEditManager EditManager => _editManager ??= new EditManager();
}
