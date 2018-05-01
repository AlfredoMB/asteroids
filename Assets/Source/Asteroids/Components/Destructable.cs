using AlfredoMB.ServiceLocator;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public void Destroy()
    {
        ServiceLocator.Get<IGameObjectSpawner>().Despawn(gameObject);
    }
}