using MyGameEngine.Core.Extensions;

namespace MyGameEngine.Core.Models;

public class Shape2D : GameObject
{
    public Color Fill { get; set; }

    public override void Draw(Graphics g, int levelsDown = 0)
    {
        if (levelsDown > 0)
        {
            var color = Fill.AlterBrightness(-.3f * levelsDown);
            g.FillRectangle(new SolidBrush(color), this.Rectangle());
        }
        else
            g.FillRectangle(new SolidBrush(Fill), this.Rectangle());
    }
}
