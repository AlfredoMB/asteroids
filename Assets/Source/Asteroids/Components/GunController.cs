using AlfredoMB.ServiceLocator;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public ShotController Shot;

    private ShipModel _shipModel;
    private float _gunCooldownFinishTime;

    public void Initialize(ShipModel shipModel)
    {
        _shipModel = shipModel;
    }

    public void Fire()
    {
        if (Time.time < _gunCooldownFinishTime)
        {
            return;
        }

        _gunCooldownFinishTime = Time.time + (1 / _shipModel.FireRate);

        var shot = ServiceLocator.Get<IGameObjectSpawner>().Spawn(Shot, transform.position, Quaternion.LookRotation(transform.forward, transform.up));
    }
}