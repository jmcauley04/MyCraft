using MyGameEngine.Core.Caches;
using MyGameEngine.Core.Extensions;
using MyGameEngine.Core.Interfaces;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Controllers;

public class PlayerController : IGameController
{
    Sprite2D? _player;
    float _playerSpeed = 2f;

    bool _left;
    bool _right;
    bool _up;
    bool _down;

    static int _iterationsPerImage = 10;
    static int _iterationNumber = 0;
    static int _iteration = 0;

    public void OnUpdate()
    {
        if (_player is not null)
        {
            var oldPosition = new Vector2(_player.Position.X, _player.Position.Y);

            if (_left)
            {
                _player.Position.X -= _playerSpeed;
                _player.Sprite = ImageCache.GetImage(ImageCache.ImageId.PlayerLeft, _iterationNumber);
            }

            if (_right)
            {
                _player.Position.X += _playerSpeed;
                _player.Sprite = ImageCache.GetImage(ImageCache.ImageId.PlayerRight, _iterationNumber);
            }

            if (_up)
            {
                _player.Position.Y -= _playerSpeed;
                _player.Sprite = ImageCache.GetImage(ImageCache.ImageId.PlayerUp, _iterationNumber);
            }

            if (_down)
            {
                _player.Position.Y += _playerSpeed;
                _player.Sprite = ImageCache.GetImage(ImageCache.ImageId.PlayerDown, _iterationNumber);
            }

            if (_left || _up || _right || _down)
            {
                if (_iteration >= _iterationsPerImage)
                {
                    _iteration = 0;
                    _iterationNumber = (_iterationNumber + 1) % 4;
                }
                _iteration++;
            }

            if (_player.IsColliding("Ground") is not null)
            {
                var newPosition = new Vector2(_player.Position.X, _player.Position.Y);
                _player.Position.X = oldPosition.X;
                if (_player.IsColliding("Ground") is not null)
                {
                    _player.Position.Y = oldPosition.Y;
                    _player.Position.X = newPosition.X;

                    if (_player.IsColliding("Ground") is not null)
                    {
                        _player.Position.X = oldPosition.X;
                    }
                }
            }
        }
    }

    public void SetGameObject(BaseDrawable gameObject)
    {
        _player = gameObject as Sprite2D;
    }

    public void SetKey(string key, bool target)
    {
        switch (key)
        {
            case "A":
                _left = target;
                break;
            case "D":
                _right = target;
                break;
            case "W":
                _up = target;
                break;
            case "S":
                _down = target;
                break;
        }
    }
}
