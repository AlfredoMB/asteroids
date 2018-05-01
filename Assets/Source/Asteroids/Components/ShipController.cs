using System;
using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public MainThrusterController _mainThruster;
    public SideThrusterController _leftThruster;
    public SideThrusterController _rightThruster;

    public GunController _gun;

    private ICommandController _commandController;

    private void OnEnable()
    {
        // add listeners to commands
        _commandController = ServiceLocator.Get<ICommandController>();
        _commandController.AddListener<StartThrustersCommand>(OnStartMainThrustersCommand);
        _commandController.AddListener<StopThrustersCommand>(OnStopMainThrustersCommand);

        _commandController.AddListener<StartLeftThrustersCommand>(OnStartLeftThrustersCommand);
        _commandController.AddListener<StopLeftThrustersCommand>(OnStopLeftThrustersCommand);

        _commandController.AddListener<StartRightThrustersCommand>(OnStartRightThrustersCommand);
        _commandController.AddListener<StopRightThrustersCommand>(OnStopRightThrustersCommand);

        _commandController.AddListener<FireCommand>(OnFireCommand);
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
        if (_commandController == null)
        {
            return;
        }

        _commandController.RemoveListener<StartThrustersCommand>(OnStartMainThrustersCommand);
        _commandController.RemoveListener<StopThrustersCommand>(OnStopMainThrustersCommand);

        _commandController.RemoveListener<StartLeftThrustersCommand>(OnStartLeftThrustersCommand);
        _commandController.RemoveListener<StopLeftThrustersCommand>(OnStopLeftThrustersCommand);

        _commandController.RemoveListener<StartRightThrustersCommand>(OnStartRightThrustersCommand);
        _commandController.RemoveListener<StopRightThrustersCommand>(OnStopRightThrustersCommand);

        _commandController.RemoveListener<FireCommand>(OnFireCommand);
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
