using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public void ExecuteDestruction(GameObject destroyer = null)
    {
        ServiceLocator.Get<IGameObjectSpawner>().Despawn(gameObject);
        ServiceLocator.Get<ICommandController>().AddCommand(new DestroyCommand(gameObject, destroyer));
    }
}