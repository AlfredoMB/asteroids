using System;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public event Action<GameObject> OnDestroyed;

    private BaseGameObjectSpawner _spawner;

    public void Initialize(BaseGameObjectSpawner spawner)
    {
        _spawner = spawner;
    }

    public void ExecuteDestruction(GameObject destroyer = null)
    {
        _spawner.Despawn(gameObject);
        if (OnDestroyed != null)
        {
            OnDestroyed(destroyer);
        }
    }
}