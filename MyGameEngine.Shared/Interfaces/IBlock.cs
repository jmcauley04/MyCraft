namespace MyGameEngine.Shared.Interfaces
{
    public interface IBlock : IRenderableGameObject
    {
        void Place(int x, int y);
        int Width { get; }
        int Height { get; }
    }
}
