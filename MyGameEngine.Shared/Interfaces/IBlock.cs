namespace MyGameEngine.Shared.Interfaces
{
    public interface IBlock : IRenderableGameObject
    {
        int XPosition { get; }
        int YPosition { get; }
        int Width { get; }
        int Height { get; }
        int ZIndex { get; }
        void Place(int x, int y);
    }
}
