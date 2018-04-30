using UnityEngine;

public class GameObjectSpawner : IGameObjectSpawner
{
    public T Spawn<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Object.Instantiate(original, position, rotation);
    }

    public T Spawn<T>(T shot) where T : Object
    {
        return Spawn(shot, Vector3.zero, Quaternion.identity);
    }

    public void Despawn(GameObject instantiatedObject)
    {
        Object.Destroy(instantiatedObject);
    }
}
