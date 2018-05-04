using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public ScreenWrapper ScreenWrapper;

    public MainThrusterController _mainThruster;
    public SideThrusterController _leftThruster;
    public SideThrusterController _rightThruster;
    public GunController _gun;

    private BaseShipInput _shipInput;
    private BaseCamera _camera;

    public event Action<GameObject, GameObject> OnShipDestruction;

    public void Initialize(ShipModel shipModel, BaseGameObjectSpawner spawner, BaseCamera camera)
    {
        _camera = camera;

        _mainThruster.Initialize(shipModel.MainThrusterStrength, Rigidbody);
        _leftThruster.Initialize(shipModel.SideThrusterStrength, Rigidbody);
        _rightThruster.Initialize(shipModel.SideThrusterStrength, Rigidbody);
        _gun.Initialize(shipModel.FireRate, spawner, camera);
        
        ScreenWrapper.Initialize(camera);
    }

    private void OnEnable()
    {
        if(_shipInput == null)
        {
            return;
        }

        SetInput(_shipInput);
    }

    public void SetInput(BaseShipInput shipInput)
    {
        _shipInput = shipInput;
        shipInput.OnStartMainThrusters += _mainThruster.StartThruster;
        shipInput.OnStopMainThrusters += _mainThruster.StopThruster;

        shipInput.OnStartLeftThrusters += _leftThruster.StartThruster;
        shipInput.OnStopLeftThrusters += _leftThruster.StopThruster;

        shipInput.OnStartRightThrusters += _rightThruster.StartThruster;
        shipInput.OnStopRightThrusters += _rightThruster.StopThruster;

        shipInput.OnFire += _gun.Fire;
        shipInput.OnHyperspaceJump += OnHyperspaceJump;
    }
    
    private void OnDisable()
    {
        _mainThruster.StopThruster();
        _leftThruster.StopThruster();
        _rightThruster.StopThruster();

        if (_shipInput == null)
        {
            return;
        }
        _shipInput.OnStartMainThrusters -= _mainThruster.StartThruster;
        _shipInput.OnStopMainThrusters -= _mainThruster.StopThruster;

        _shipInput.OnStartLeftThrusters -= _leftThruster.StartThruster;
        _shipInput.OnStopLeftThrusters -= _leftThruster.StopThruster;

        _shipInput.OnStartRightThrusters -= _rightThruster.StartThruster;
        _shipInput.OnStopRightThrusters -= _rightThruster.StopThruster;

        _shipInput.OnFire -= _gun.Fire;
        _shipInput.OnHyperspaceJump -= OnHyperspaceJump;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (OnShipDestruction != null)
        {
            OnShipDestruction(gameObject, collision.gameObject);
        }
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

    private void OnHyperspaceJump()
    {
        Respawn(GetRandomPositionInTheScreen());
    }

    private Vector3 GetRandomPositionInTheScreen()
    {
        float z = _camera.transform.position.y;
        Vector3 randomPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), z);
        return _camera.ViewportToWorldPoint(randomPosition);
    }
}
