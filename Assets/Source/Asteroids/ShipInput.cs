using AlfredoMB.Command;
using AlfredoMB.DI;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    private ICommandController _commandController;
    private StartThrusters _startThrusters = new StartThrusters();
    private StopThrusters _stopThrusters = new StopThrusters();
    private bool _areThrustersOn;

    private StartLeftThrusters _startLeftThrusters = new StartLeftThrusters();
    private StopLeftThrusters _stopLeftThrusters = new StopLeftThrusters();
    private bool _areLeftThrustersOn;

    private StartRightThrusters _startRightThrusters = new StartRightThrusters();
    private StopRightThrusters _stopRightThrusters = new StopRightThrusters();
    private bool _areRightThrustersOn;

    private void Start()
    {
        _commandController = SimpleDI.Get<ICommandController>();        
    }

    private void Update()
    {
        var thrusterAxis = Input.GetAxisRaw("Vertical");
        if (thrusterAxis >= 1 && !_areThrustersOn)
        {
            _areThrustersOn = true;
            _commandController.AddCommand(_startThrusters);
        }
        else if (thrusterAxis < 1 && _areThrustersOn)
        {
            _areThrustersOn = false;
            _commandController.AddCommand(_stopThrusters);
        }


        thrusterAxis = Input.GetAxisRaw("Horizontal");
        if (thrusterAxis >= 1 && !_areLeftThrustersOn)
        {
            _areLeftThrustersOn = true;
            _commandController.AddCommand(_startLeftThrusters);
        }
        else if (thrusterAxis < 1 && _areLeftThrustersOn)
        {
            _areLeftThrustersOn = false;
            _commandController.AddCommand(_stopLeftThrusters);
        }

        if (thrusterAxis <= -1 && !_areRightThrustersOn)
        {
            _areRightThrustersOn = true;
            _commandController.AddCommand(_startRightThrusters);
        }
        else if (thrusterAxis > -1 && _areRightThrustersOn)
        {
            _areRightThrustersOn = false;
            _commandController.AddCommand(_stopRightThrusters);
        }
    }
}
