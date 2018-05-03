using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Destructable Destructable;
    public ScreenWrapper ScreenWrapper;

    public MainThrusterController _mainThruster;
    public SideThrusterController _leftThruster;
    public SideThrusterController _rightThruster;
    public GunController _gun;

    public event Action OnShipDestruction;

    public void Initialize(ShipModel shipModel, BaseGameObjectSpawner spawner, BaseCamera camera)
    {
        _mainThruster.Initialize(shipModel, Rigidbody);
        _leftThruster.Initialize(shipModel, Rigidbody);
        _rightThruster.Initialize(shipModel, Rigidbody);
        _gun.Initialize(shipModel, spawner, camera);

        Destructable.Initialize(spawner);
        ScreenWrapper.Initialize(camera);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (OnShipDestruction != null)
        {
            OnShipDestruction();
        }
        Destructable.ExecuteDestruction(collision.gameObject);
    }

    public void StartMainThrusters()
    {
        _mainThruster.StartThruster();
    }

    public void StopMainThrusters()
    {
        _mainThruster.StopThruster();
    }

    public void StartLeftThrusters()
    {
        _leftThruster.StartThruster();
    }

    public void StopLeftThrusters()
    {
        _leftThruster.StopThruster();
    }

    public void StartRightThrusters()
    {
        _rightThruster.StartThruster();
    }

    public void StopRightThrusters()
    {
        _rightThruster.StopThruster();
    }

    public void Fire()
    {
        _gun.Fire();
    }
}
