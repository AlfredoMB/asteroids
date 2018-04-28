using UnityEngine;

public class GameObjectSpawner : IGameObjectSpawner
{
    public GameObject Spawn(GameObject original, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(original, position, rotation);
    }

    public void Despawn(GameObject instantiatedObject)
    {
        GameObject.Destroy(instantiatedObject);
    }
}
