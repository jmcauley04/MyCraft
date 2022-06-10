using MyCraft2D;
using MyGameEngine.Core.Extensions;
using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Controllers;

public class MyCraftPlayerController : PlayerController
{
    static int _iterationsPerImage = 10;
    static int _iterationNumber = 0;
    static int _iteration = 0;

    float _playerSpeed = 2f;

    bool _left;
    bool _right;
    bool _up;
    bool _down;

    public override void OnUpdate()
    {
        if (_player is not null)
        {
            UpdateAnimation();
            UpdateMovement();
        }
    }

    public override void SetKey(string key, bool target)
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

    private void UpdateMovement()
    {
        if (_player is null) return;

        var oldPosition = new Vector2(_player.Position.X, _player.Position.Y);

        if (_left)
        {
            _player.Position.X -= _playerSpeed;
        }

        if (_right)
        {
            _player.Position.X += _playerSpeed;
        }

        if (_up)
        {
            _player.Position.Y -= _playerSpeed;
        }

        if (_down)
        {
            _player.Position.Y += _playerSpeed;
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

    private void UpdateAnimation()
    {
        if (_player is null) return;

        if (_left)
        {
            _player.Sprite = MyCraftImageHandler.GetImage(ImageId.PlayerLeft, _iterationNumber);
        }

        if (_right)
        {
            _player.Sprite = MyCraftImageHandler.GetImage(ImageId.PlayerRight, _iterationNumber);
        }

        if (_up)
        {
            _player.Sprite = MyCraftImageHandler.GetImage(ImageId.PlayerUp, _iterationNumber);
        }

        if (_down)
        {
            _player.Sprite = MyCraftImageHandler.GetImage(ImageId.PlayerDown, _iterationNumber);
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
    }
}
