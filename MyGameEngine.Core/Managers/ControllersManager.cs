using MyGameEngine.Core.Interfaces;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Managers;

public class ControllersManager
{
    List<IGameController> _controllers = new List<IGameController>();

    Action? OnUpdate;
    Action<string, bool>? OnSetKey;

    public IGameController LoadController<T>(BaseDrawable gameObject) where T : IGameController, new()
    {
        var ctrl = new T();
        ctrl.SetGameObject(gameObject);
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
