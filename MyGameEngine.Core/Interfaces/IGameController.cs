using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Interfaces;

public interface IGameController
{
    public void SetMouse(string button, bool target, Vector2 coords);
    public void SetKey(string key, bool target);
    public void OnUpdate();
}
