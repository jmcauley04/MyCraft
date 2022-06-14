using MyGameEngine.Core.Interfaces;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Managers;

public class ControllersManager
{
    readonly List<IGameController> _controllers = new();

    Action? OnUpdate;
    Action<string, bool>? OnSetKey;
    Action<string, bool, Vector2>? OnSetMouse;

    public T LoadController<T>() where T : IGameController, new()
    {
        var ctrl = new T();
        OnUpdate += ctrl.OnUpdate;
        OnSetKey += ctrl.SetKey;
        OnSetMouse += ctrl.SetMouse;
        _controllers.Add(ctrl);
        return ctrl;
    }

    public void UnloadController(IGameController ctrl)
    {
        OnUpdate -= ctrl.OnUpdate;
        OnSetKey -= ctrl.SetKey;
        OnSetMouse -= ctrl.SetMouse;
        _controllers.Remove(ctrl);
    }

    public void Update()
    {
        OnUpdate?.Invoke();
    }

    public void SetKey(string key, bool target)
    {
        OnSetKey?.Invoke(key, target);
    }

    internal void SetMouse(string button, bool isDown, Vector2 coords)
    {
        OnSetMouse?.Invoke(button, isDown, coords);
    }
}
