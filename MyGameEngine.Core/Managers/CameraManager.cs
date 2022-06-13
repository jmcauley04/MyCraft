using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Managers;

public static class CameraManager
{
    private static Vector2 _screensize = Vector2.Zero();
    private static Camera? _activeCamera;
    public static Camera? ActiveCamera => _activeCamera;

    static readonly Dictionary<string, Camera> _cameras = new();

    public static void RegisterCamera(Camera camera)
    {
        _cameras[camera.Tag] = camera;
        _activeCamera ??= camera;
    }

    public static void SetScreensize(Vector2 screensize)
    {
        _screensize = screensize;
    }

    public static void UnregisterCamera(string tag)
    {
        _cameras.Remove(tag);
        _activeCamera = _cameras.Count > 0 ? _cameras.First().Value : null;
    }

    public static void SetActiveCamera(string tag)
    {
        if (_cameras.TryGetValue(tag, out Camera? camera))
            _activeCamera = camera;
        else
            Log.Error($"Can't find camera with tag: {tag}");
    }

    public static void UpdateActiveCamera()
    {
        if (_activeCamera is not null && _activeCamera.Target is not null)
        {
            var target = _activeCamera.Target.Position - _screensize / 2f + _activeCamera.Target.Scale / 2f;
            _activeCamera.Update(target.X, target.Y);
        }
    }
}
