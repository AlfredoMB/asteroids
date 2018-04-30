using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    private ICommandController _commandController;

    private StartThrustersCommand _startThrusters = new StartThrustersCommand();
    private StopThrustersCommand _stopThrusters = new StopThrustersCommand();
    private bool _areThrustersOn;

    private StartLeftThrustersCommand _startLeftThrusters = new StartLeftThrustersCommand();
    private StopLeftThrustersCommand _stopLeftThrusters = new StopLeftThrustersCommand();
    private bool _areLeftThrustersOn;

    private StartRightThrustersCommand _startRightThrusters = new StartRightThrustersCommand();
    private StopRightThrustersCommand _stopRightThrusters = new StopRightThrustersCommand();
    private bool _areRightThrustersOn;

    private FireCommand _fireCommand = new FireCommand();

    private void Start()
    {
        _commandController = ServiceLocator.Get<ICommandController>();        
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

        if (Input.GetButton("Fire"))
        {
            _commandController.AddCommand(_fireCommand);
        }
    }
}
