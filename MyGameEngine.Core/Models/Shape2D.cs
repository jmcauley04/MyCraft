using MyGameEngine.Core.Extensions;

namespace MyGameEngine.Core.Models;

public class Shape2D : GameObject
{
    public Color Fill { get; set; }

    public override void Draw(Graphics g)
    {
        g.FillRectangle(new SolidBrush(Fill), this.Rectangle());
    }
}
