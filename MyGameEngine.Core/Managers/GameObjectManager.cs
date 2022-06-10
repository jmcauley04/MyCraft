using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Managers;

public static class GameObjectManager
{
    private static Dictionary<Type, HashSet<GameObject>> _dictionary = new();

    public static IEnumerable<GameObject> GetGameObjects()
    {
        foreach (var kvp in _dictionary)
            foreach (var gameobject in kvp.Value)
                yield return gameobject;
    }

    public static IEnumerable<T> GetGameObjects<T>() where T : GameObject
    {
        return GetGameObjects()
            .Where(x => x.GetType() == typeof(T))
            .Cast<T>();
    }

    public static void RegisterGameObject<T>(T gameObject) where T : GameObject
    {
        var type = gameObject.GetType();

        if (!_dictionary.ContainsKey(type))
            _dictionary.Add(type, new HashSet<GameObject>());

        _dictionary[type].Add(gameObject);
    }

    public static void UnregisterGameObject<T>(T gameObject) where T : GameObject
    {
        var type = gameObject.GetType();

        _dictionary[type].Remove(gameObject);

        if (_dictionary[type].Count == 0)
            _dictionary.Remove(type);
    }
}
