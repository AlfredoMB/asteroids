using System;
using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Destructable Destructable;

    public MainThrusterController _mainThruster;
    public SideThrusterController _leftThruster;
    public SideThrusterController _rightThruster;

    public GunController _gun;

    private void OnEnable()
    {
        // add listeners to commands
        var commandController = ServiceLocator.Get<ICommandController>();
        commandController.AddListener<StartThrustersCommand>(OnStartMainThrustersCommand);
        commandController.AddListener<StopThrustersCommand>(OnStopMainThrustersCommand);

        commandController.AddListener<StartLeftThrustersCommand>(OnStartLeftThrustersCommand);
        commandController.AddListener<StopLeftThrustersCommand>(OnStopLeftThrustersCommand);

        commandController.AddListener<StartRightThrustersCommand>(OnStartRightThrustersCommand);
        commandController.AddListener<StopRightThrustersCommand>(OnStopRightThrustersCommand);

        commandController.AddListener<FireCommand>(OnFireCommand);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destructable.ExecuteDestruction(collision.gameObject);
    }
    
    public void Initialize(ShipModel shipModel)
    {
        _mainThruster.Initialize(shipModel, Rigidbody);
        _leftThruster.Initialize(shipModel, Rigidbody);
        _rightThruster.Initialize(shipModel, Rigidbody);
        _gun.Initialize(shipModel);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        var commandController = ServiceLocator.Get<ICommandController>();
        commandController.RemoveListener<StartThrustersCommand>(OnStartMainThrustersCommand);
        commandController.RemoveListener<StopThrustersCommand>(OnStopMainThrustersCommand);

        commandController.RemoveListener<StartLeftThrustersCommand>(OnStartLeftThrustersCommand);
        commandController.RemoveListener<StopLeftThrustersCommand>(OnStopLeftThrustersCommand);

        commandController.RemoveListener<StartRightThrustersCommand>(OnStartRightThrustersCommand);
        commandController.RemoveListener<StopRightThrustersCommand>(OnStopRightThrustersCommand);

        commandController.RemoveListener<FireCommand>(OnFireCommand);
    }

    private void OnStartMainThrustersCommand(ICommand obj)
    {
        _mainThruster.StartThruster();
    }

    private void OnStopMainThrustersCommand(ICommand obj)
    {
        _mainThruster.StopThruster();
    }

    private void OnStartLeftThrustersCommand(ICommand obj)
    {
        _leftThruster.StartThruster();
    }

    private void OnStopLeftThrustersCommand(ICommand obj)
    {
        _leftThruster.StopThruster();
    }

    private void OnStartRightThrustersCommand(ICommand obj)
    {
        _rightThruster.StartThruster();
    }

    private void OnStopRightThrustersCommand(ICommand obj)
    {
        _rightThruster.StopThruster();
    }

    private void OnFireCommand(ICommand obj)
    {
        _gun.Fire();
    }
}
