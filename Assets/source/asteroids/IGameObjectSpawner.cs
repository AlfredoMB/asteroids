using UnityEngine;

public interface IGameObjectSpawner
{
    T Spawn<T>(T original, Vector3 position, Quaternion rotation) where T : Object;
    T Spawn<T>(T shot) where T : Object;
    void Despawn(GameObject instantiatedObject);
}