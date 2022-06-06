using MyGameEngine.Shared.Records;

namespace MyGameEngine.Shared.Interfaces;

public interface IRenderableGameObject
{
    int XPosition { get; }
    int YPosition { get; }
    int ZIndex { get; }
    ColorRecord[,] GetPixels();
    bool ShouldRender();
    void Render();
}
