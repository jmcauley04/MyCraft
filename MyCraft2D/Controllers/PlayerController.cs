using MyCraft2D.GameObjects.Tiles;
using MyGameEngine.Core;
using MyGameEngine.Core.Controllers;
using MyGameEngine.Core.Extensions;
using MyGameEngine.Core.Managers;
using MyGameEngine.Core.Models;
using System.Diagnostics;

namespace MyCraft2D.Controllers;

public class PlayerController : GameController
{
    static int _iterationsPerImage = 10;
    static int _iterationNumber = 0;
    static int _iteration = 0;
    static Stopwatch _stopWatch = new();

    Player? _player;
    float _basePlayerSpeed = 180f;
    float _maxReach = 100f;
    float _playerSpeed => _sprint ? _basePlayerSpeed * 1.5f : _basePlayerSpeed;

    bool _left;
    bool _right;
    bool _up;
    bool _down;
    bool _sprint;

    public override void OnUpdate()
    {
        if (_player is not null)
        {
            UpdateAnimation();
            UpdateMovement();
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
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
            case "ShiftKey":
                _sprint = target;
                break;
        }
    }

    public override void SetMouse(string button, bool isDown, Vector2 coords)
    {
        if (_player is null)
            return;

        if (isDown)
        {
            var distance = coords.DistanceFrom(_player.Position + (_player.Scale / 2f));

            if (distance <= _maxReach)
            {
                var collisions = GameObjectManager.GetCollisionsAt(coords);
                // try to mine
                if (button == "Left")
                {
                    var hit = collisions.LastOrDefault();
                    //TODO: Perhaps an interface that decides that things are mineable?  IMineable
                    if (hit is not null && hit.Tag == Tags.Ground)
                        hit.DestroySelf();
                }
                // try to build
                else if (button == "Right" && !collisions.Any())
                {
                    var x = (float)Math.Floor(coords.X / Settings.TileSize) * Settings.TileSize;
                    var y = (float)Math.Floor(coords.Y / Settings.TileSize) * Settings.TileSize;

                    var shape = GroundObject.Build(x, y);

                    if (shape.IsColliding(Tags.Player) is not null)
                        shape.DestroySelf();
                }
            }
            else
                Log.Info($"I can't reach it! {distance}");
        }
    }

    private void UpdateMovement()
    {
        if (_player is null || !_player.IsAlive) return;

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

        var t = (float)_stopWatch.Elapsed.TotalSeconds;
        _player.Position.BoundDistanceFrom(oldPosition, _playerSpeed * t);
        _stopWatch.Restart();

        if (_player.IsColliding(Tags.Ground) is not null)
        {
            var newPosition = new Vector2(_player.Position.X, _player.Position.Y);
            _player.Position.X = oldPosition.X;
            if (_player.IsColliding(Tags.Ground) is not null)
            {
                _player.Position.Y = oldPosition.Y;
                _player.Position.X = newPosition.X;

                if (_player.IsColliding(Tags.Ground) is not null)
                {
                    _player.Position.X = oldPosition.X;
                }
            }
        }
    }

    private void UpdateAnimation()
    {
        if (_player is null) return;

        // If Dead
        if (!_player.IsAlive)
        {
            _player.Sprite = MyCraftImageHandler.GetImage(ImageId.PlayerRight, _iterationNumber);
            return;
        }

        // Else animate movement
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

        // Iterate the animation index
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
