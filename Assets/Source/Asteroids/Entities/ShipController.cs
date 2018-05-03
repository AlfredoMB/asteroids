using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    //public Destructable Destructable;
    public ScreenWrapper ScreenWrapper;

    public MainThrusterController _mainThruster;
    public SideThrusterController _leftThruster;
    public SideThrusterController _rightThruster;
    public GunController _gun;

    private BaseShipInput _shipInput;

    public event Action<GameObject, GameObject> OnShipDestruction;

    public void Initialize(ShipModel shipModel, BaseGameObjectSpawner spawner, BaseCamera camera)
    {
        _mainThruster.Initialize(shipModel, Rigidbody);
        _leftThruster.Initialize(shipModel, Rigidbody);
        _rightThruster.Initialize(shipModel, Rigidbody);
        _gun.Initialize(shipModel, spawner, camera);

        //Destructable.Initialize(spawner);
        ScreenWrapper.Initialize(camera);
    }

    public void SetInput(BaseShipInput shipInput)
    {
        _shipInput = shipInput;
        shipInput.OnStartMainThrusters += StartMainThrusters;
        shipInput.OnStopMainThrusters += StopMainThrusters;

        shipInput.OnStartLeftThrusters += StartLeftThrusters;
        shipInput.OnStopLeftThrusters += StopLeftThrusters;

        shipInput.OnStartRightThrusters += StartRightThrusters;
        shipInput.OnStopRightThrusters += StopRightThrusters;

        shipInput.OnFire += Fire;
    }

    private void OnDestroy()
    {
        if (_shipInput == null)
        {
            return;
        }

        _shipInput.OnStartMainThrusters -= StartMainThrusters;
        _shipInput.OnStopMainThrusters -= StopMainThrusters;

        _shipInput.OnStartLeftThrusters -= StartLeftThrusters;
        _shipInput.OnStopLeftThrusters -= StopLeftThrusters;

        _shipInput.OnStartRightThrusters -= StartRightThrusters;
        _shipInput.OnStopRightThrusters -= StopRightThrusters;

        _shipInput.OnFire += Fire;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        //Destructable.OnDestruction += OnShipDestruction;
        //Destructable.ExecuteDestruction(collision.gameObject);
        if (OnShipDestruction != null)
        {
            OnShipDestruction(gameObject, collision.gameObject);
        }
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

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Respawn(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, Quaternion.LookRotation(Vector3.forward));
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
    }
}
