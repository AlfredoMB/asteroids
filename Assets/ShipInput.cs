using AlfredoMB.Command;
using AlfredoMB.DI;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    private ICommandController _commandController;
    private StartThrusters _startThrusters = new StartThrusters();
    private StopThrusters _stopThrusters = new StopThrusters();
    private bool _areThrustersOn;

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
    }
}
