using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameObjectSpawner : MonoBehaviour
{
    public abstract GameObject Spawn(GameObject original, Transform parent = null);
    public abstract GameObject Spawn(GameObject original, Vector3 position, Transform parent = null);
    public abstract GameObject Spawn(GameObject original, Vector3 position, Quaternion rotation, Transform parent = null);
    public abstract void Despawn(GameObject instantiatedObject);
    public abstract void Reset(List<GameObject> exceptionList = null);
}