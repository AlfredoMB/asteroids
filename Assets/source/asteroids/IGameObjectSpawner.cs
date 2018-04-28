using UnityEngine;

public interface IGameObjectSpawner
{
    void Despawn(GameObject instantiatedObject);
    GameObject Spawn(GameObject original, Vector3 position, Quaternion rotation);
}