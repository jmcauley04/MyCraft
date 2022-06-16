namespace MyGameEngine.Core.Models;

public class Sprite2D : GameObject
{
    public Bitmap Sprite { get; set; }

    public override void Draw(Graphics g, int levelsDown = 0)
    {
        g.DrawImage(Sprite, Position.X, Position.Y, Scale.X, Scale.Y);
    }
}
