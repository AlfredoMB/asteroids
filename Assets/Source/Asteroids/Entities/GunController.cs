using UnityEngine;

public class GunController : MonoBehaviour
{
    public ShotController Shot;

    private ShipModel _shipModel;
    private BaseGameObjectSpawner _spawner;
    private BaseCamera _camera;

    private float _gunCooldownFinishTime;

    public void Initialize(ShipModel shipModel, BaseGameObjectSpawner spawner, BaseCamera camera)
    {
        _shipModel = shipModel;
        _spawner = spawner;
        _camera = camera;
    }

    public void Fire()
    {
        if (Time.time < _gunCooldownFinishTime)
        {
            return;
        }

        _gunCooldownFinishTime = Time.time + (1 / _shipModel.FireRate);

        var shot = _spawner.Spawn(Shot.gameObject, transform.position, Quaternion.LookRotation(transform.forward, transform.up));
        shot.GetComponent<ShotController>().Initialize(_spawner, _camera);        
    }
}