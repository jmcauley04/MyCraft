using MyGameEngine.Shared.Interfaces;
using MyGameEngine.Shared.Records;

namespace MyGameEngine.Shared;

public interface IViewManager
{
    public Action<ColorRecord[,,]>? OnViewChanged { get; set; }
    public int ResolutionX { get; }
    public int ResolutionY { get; }
    public ColorRecord[,,] GetView();
    void PlaceBlockAt<T>(int posX, int posY) where T : IBlock, new();
}