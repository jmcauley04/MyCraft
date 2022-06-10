using MyGameEngine.Shared.Models;

namespace MyGameEngine.Shared.Interfaces;

public interface IGameController
{
    public void SetGameObject(BaseDrawable gameObject);
    public void SetKey(string key, bool target);
    public void OnUpdate();
}
