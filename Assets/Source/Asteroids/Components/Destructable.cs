using System;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public event Action<GameObject, GameObject> OnDestruction;

    private BaseGameObjectSpawner _spawner;

    public void Initialize(BaseGameObjectSpawner spawner)
    {
        _spawner = spawner;
    }

    public void ExecuteDestruction(GameObject destroyer = null)
    {
        if (OnDestruction != null)
        {
            OnDestruction(gameObject, destroyer);
        }
        _spawner.Despawn(gameObject);
    }
}