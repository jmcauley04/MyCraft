using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Interfaces;

public interface IGameController
{
    public void SetGameObject(BaseDrawable gameObject);
    public void SetKey(string key, bool target);
    public void OnUpdate();
}
