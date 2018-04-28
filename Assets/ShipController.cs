using AlfredoMB.Command;
using AlfredoMB.DI;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;

    private ICommandController _commandController;

    private bool areThrustersOn;
    private const float _thrusterStrength = 10f;

    private void Start()
    {
        // add listeners to commands
        _commandController = SimpleDI.Get<ICommandController>();
        _commandController.AddListener<StartThrusters>(OnStartThrusters);
        _commandController.AddListener<StopThrusters>(OnStopThrusters);
    }

    private void OnDestroy()
    {
        if (_commandController == null)
        {
            return;
        }

        _commandController.RemoveListener<StartThrusters>(OnStartThrusters);
        _commandController.RemoveListener<StopThrusters>(OnStopThrusters);
    }

    private void OnStartThrusters(ICommand obj)
    {
        Debug.Log("Thrusters ON!");
        areThrustersOn = true;
    }

    private void OnStopThrusters(ICommand obj)
    {
        Debug.Log("Thrusters off.");
        areThrustersOn = false;
    }

    private void FixedUpdate()
    {
        if (areThrustersOn)
        {
            Rigidbody.AddForce(transform.forward * _thrusterStrength);
        }
    }
}
