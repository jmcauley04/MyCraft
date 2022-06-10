using MyGameEngine.Core.Interfaces;

namespace MyGameEngine.Core.Managers;

public class ControllersManager
{
    List<IGameController> _controllers = new();

    Action? OnUpdate;
    Action<string, bool>? OnSetKey;

    public T LoadController<T>() where T : IGameController, new()
    {
        var ctrl = new T();
        OnUpdate += ctrl.OnUpdate;
        OnSetKey += ctrl.SetKey;
        _controllers.Add(ctrl);
        return ctrl;
    }

    public void UnloadController(IGameController ctrl)
    {
        OnUpdate -= ctrl.OnUpdate;
        OnSetKey -= ctrl.SetKey;
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
}
