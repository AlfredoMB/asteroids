using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : BaseGameObjectSpawner
{
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private class PoolEntry
    {
        public GameObject Instance;
        public bool Available;
    }
    private Dictionary<GameObject, List<PoolEntry>> _pools = new Dictionary<GameObject, List<PoolEntry>>();

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
        var spawned = GetInstance(original);
        spawned.transform.SetParent(parent ?? transform);
        spawned.transform.SetPositionAndRotation(position, rotation);
        _spawnedObjects.Add(spawned);
        return spawned;
    }

    private GameObject GetInstance(GameObject original)
    {
        List<PoolEntry> pool;
        if (!_pools.TryGetValue(original, out pool))
        {
            var instance = Instantiate(original);
            _pools.Add(original, new List<PoolEntry>() { new PoolEntry { Instance = instance, Available = false } });
            instance.SetActive(true);
            return instance;
        }

        var availableEntry = pool.Find(entry => entry.Available);
        if (availableEntry == null)
        {
            var instance = Instantiate(original);
            availableEntry = new PoolEntry { Instance = instance, Available = true };
            pool.Add(availableEntry);
        }
        availableEntry.Available = false;
        availableEntry.Instance.SetActive(true);
        return availableEntry.Instance;
    }

    public override void Despawn(GameObject instance)
    {
        ReturnInstance(instance);
        _spawnedObjects.Remove(instance);
    }

    private void ReturnInstance(GameObject instance)
    {
        var rigidBody = instance.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }

        instance.SetActive(false);
        instance.transform.parent = transform;
        
        foreach (var poolEntryList in _pools.Values)
        {
            var instancePoolEntry = poolEntryList.Find(entry => entry.Instance == instance);
            if (instancePoolEntry != null)
            {
                instancePoolEntry.Available = true;
                break;
            }
        }
    }

    public override void Reset(List<GameObject> exceptionList = null)
    {
        foreach(var spawned in _spawnedObjects)
        {
            if (exceptionList.Contains(spawned))
            {
                continue;
            }

            ReturnInstance(spawned);
        }
        _spawnedObjects.Clear();
    }    
}
