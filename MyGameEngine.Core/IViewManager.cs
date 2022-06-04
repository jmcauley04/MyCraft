using MyGameEngine.Shared.Records;

namespace MyGameEngine.Core;

public interface IViewManager
{
    public Action<ColorRecord[,,]>? OnViewChanged { get; set; }
    public int ResolutionX { get; }
    public int ResolutionY { get; }
    public ColorRecord[,,] GetView();
    public void UpdateView(ColorRecord[,,] view);
}