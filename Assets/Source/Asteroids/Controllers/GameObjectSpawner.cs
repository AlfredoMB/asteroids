using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : BaseGameObjectSpawner
{
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public override GameObject Spawn(GameObject original, Transform parent = null)
    {
        return Spawn(original, Vector3.zero, Quaternion.identity, parent);
    }

    public override GameObject Spawn(GameObject original, Vector3 position, Transform parent = null)
    {
        return Spawn(original, position, Quaternion.identity, parent);
    }

    public override GameObject Spawn(GameObject original, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        var spawned = Instantiate(original, position, rotation, parent ?? transform);
        _spawnedObjects.Add(spawned);
        return spawned;
    }

    public override void Despawn(GameObject instantiatedObject)
    {
        _spawnedObjects.Remove(instantiatedObject);
        Destroy(instantiatedObject);
    }

    public override void Reset(List<GameObject> exceptionList = null)
    {
        foreach(var spawnedObject in _spawnedObjects)
        {
            if (exceptionList != null && exceptionList.Contains(spawnedObject))
            {
                continue;
            }
            Destroy(spawnedObject);
        }
        _spawnedObjects.Clear();
    }
}
