using MyGameEngine.Shared.Interfaces;

namespace MyGameEngine.Core
{
    public static class ServiceProvider
    {
        static IGameManager? _gameManager;
        public static IGameManager GameManager
        {
            get
            {
                if (_gameManager is null)
                    _gameManager = new GameManager();

                return _gameManager;
            }
        }
    }
}
