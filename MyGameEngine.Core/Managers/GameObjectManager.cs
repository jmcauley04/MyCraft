using MyGameEngine.Core.Models;

namespace MyGameEngine.Core.Managers;

public static class GameObjectManager
{
    private static Dictionary<int, HashSet<GameObject>> _dictionary = new();

    public static IEnumerable<GameObject> GetGameObjects()
    {
        foreach (var kvp in _dictionary.OrderBy(x => x.Key))
            foreach (var gameobject in kvp.Value)
                yield return gameobject;
    }

    public static IEnumerable<T> GetGameObjects<T>() where T : GameObject
    {
        return GetGameObjects()
            .Where(x => x.GetType() == typeof(T))
            .Cast<T>();
    }

    public static void RegisterGameObject(GameObject gameObject, int layer = 0)
    {
        if (!_dictionary.ContainsKey(layer))
            _dictionary[layer] = new();

        _dictionary[layer].Add(gameObject);
    }

    public static void UnregisterGameObject(GameObject gameObject)
    {
        foreach (var kvp in _dictionary)
            if (kvp.Value.Contains(gameObject))
            {
                kvp.Value.Remove(gameObject);

                if (kvp.Value.Count == 0)
                    _dictionary.Remove(kvp.Key);
            }
    }
}
