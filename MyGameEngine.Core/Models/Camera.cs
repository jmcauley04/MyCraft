namespace MyGameEngine.Core.Models;

public class Camera
{
    private Vector2 _position = Vector2.Zero();
    private float _angle = 0f;
    public Vector2 Position { get => _position; }
    public float Angle { get => _angle; }
    public string Tag { get; init; }
    public Vector2 Offset { get; init; }

    public GameObject? Target;

    public Camera(string tag, Vector2? offset = null)
    {
        Tag = tag;
        Offset = offset ?? Vector2.Zero();
    }

    public void Update(float x, float y, float angle = 0f)
    {
        _position.Update(x, y);
        _angle = angle;
    }

    public void Track(GameObject target)
    {
        Target = target;
    }
}
