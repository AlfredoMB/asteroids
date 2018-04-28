using System;
using AlfredoMB.Command;
using AlfredoMB.DI;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;

    private ICommandController _commandController;

    private bool _areThrustersOn;
    private const float _thrusterStrength = 10f;
    private const float _sideThrusterStrength = 1f;
    private bool _areLeftThrustersOn;
    private bool _areRightThrustersOn;

    private void Start()
    {
        // add listeners to commands
        _commandController = SimpleDI.Get<ICommandController>();
        _commandController.AddListener<StartThrusters>(OnStartThrusters);
        _commandController.AddListener<StopThrusters>(OnStopThrusters);

        _commandController.AddListener<StartLeftThrusters>(OnStartLeftThrusters);
        _commandController.AddListener<StopLeftThrusters>(OnStopLeftThrusters);

        _commandController.AddListener<StartRightThrusters>(OnStartRightThrusters);
        _commandController.AddListener<StopRightThrusters>(OnStopRightThrusters);
    }

    private void OnDestroy()
    {
        if (_commandController == null)
        {
            return;
        }

        _commandController.RemoveListener<StartThrusters>(OnStartThrusters);
        _commandController.RemoveListener<StopThrusters>(OnStopThrusters);

        _commandController.RemoveListener<StartLeftThrusters>(OnStartLeftThrusters);
        _commandController.RemoveListener<StopLeftThrusters>(OnStopLeftThrusters);

        _commandController.RemoveListener<StartRightThrusters>(OnStartRightThrusters);
        _commandController.RemoveListener<StopRightThrusters>(OnStopRightThrusters);
    }

    private void OnStartThrusters(ICommand obj)
    {
        Debug.Log("Thrusters ON!");
        _areThrustersOn = true;
    }

    private void OnStopThrusters(ICommand obj)
    {
        Debug.Log("Thrusters off.");
        _areThrustersOn = false;
    }

    private void OnStartLeftThrusters(ICommand obj)
    {
        Debug.Log("Left Thrusters ON!");
        _areLeftThrustersOn = true;
    }

    private void OnStopLeftThrusters(ICommand obj)
    {
        Debug.Log("Left Thrusters off.");
        _areLeftThrustersOn = false;
    }

    private void OnStartRightThrusters(ICommand obj)
    {
        Debug.Log("Right Thrusters ON!");
        _areRightThrustersOn = true;
    }

    private void OnStopRightThrusters(ICommand obj)
    {
        Debug.Log("Right Thrusters off.");
        _areRightThrustersOn = false;
    }

    private void FixedUpdate()
    {
        if (_areThrustersOn)
        {
            Rigidbody.AddForce(transform.forward * _thrusterStrength);
        }


        if (_areLeftThrustersOn)
        {
            var forcePoint = transform.position + transform.forward;
            //var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.SetPositionAndRotation(forcePoint, Quaternion.identity);
            Rigidbody.AddForceAtPosition(transform.right * _sideThrusterStrength, forcePoint);
        }
        if (_areRightThrustersOn)
        {
            var forcePoint = transform.position + transform.forward;
            //var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.SetPositionAndRotation(forcePoint, Quaternion.identity);
            Rigidbody.AddForceAtPosition(-transform.right * _sideThrusterStrength, forcePoint);
        }
    }
}
