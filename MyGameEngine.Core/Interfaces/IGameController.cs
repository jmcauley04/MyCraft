namespace MyGameEngine.Core.Interfaces;

public interface IGameController
{
    public void SetKey(string key, bool target);
    public void OnUpdate();
}
