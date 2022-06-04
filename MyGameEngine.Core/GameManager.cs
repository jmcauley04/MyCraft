using MyGameEngine.Shared.Interfaces;

namespace MyGameEngine.Core;

public class GameManager : IGameManager
{
    IPlayer? _player;

    public IPlayer GetPlayer()
    {
        if (_player is null)
            _player = new Player();

        return _player;
    }

    public void ResetGame()
    {
        // Make a new player
        // Initialize player stats
        // Initialize inventory
        // Initialize player position
        // Could some of this be done w/ extension methods?
    }
}
