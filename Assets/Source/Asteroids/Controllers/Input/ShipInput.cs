using System;
using UnityEngine;

public class ShipInput : BaseShipInput
{
    [SerializeField]
    private string MainThrustersAxisName = "Vertical";
    [SerializeField]
    private string SideThrustersAxisName = "Horizontal";
    [SerializeField]
    private string FireButton = "Fire";
    [SerializeField]
    private string HyperspaceButton = "Jump";

    private bool _aremainThrustersOn;
    private bool _areLeftThrustersOn;
    private bool _areRightThrustersOn;

    private void SafeAction(Action action)
    {
        if (action != null)
        {
            action();
        }
    }

    private void Update()
    {
        var thrusterAxis = Input.GetAxisRaw(MainThrustersAxisName);
        if (thrusterAxis >= 1 && !_aremainThrustersOn)
        {
            _aremainThrustersOn = true;
            SafeAction(OnStartMainThrusters);
        }
        else if (thrusterAxis < 1 && _aremainThrustersOn)
        {
            _aremainThrustersOn = false;
            SafeAction(OnStopMainThrusters);
        }


        thrusterAxis = Input.GetAxisRaw(SideThrustersAxisName);
        if (thrusterAxis >= 1 && !_areLeftThrustersOn)
        {
            _areLeftThrustersOn = true;
            SafeAction(OnStartLeftThrusters);
        }
        else if (thrusterAxis < 1 && _areLeftThrustersOn)
        {
            _areLeftThrustersOn = false;
            SafeAction(OnStopLeftThrusters);
        }

        if (thrusterAxis <= -1 && !_areRightThrustersOn)
        {
            _areRightThrustersOn = true;
            SafeAction(OnStartRightThrusters);
        }
        else if (thrusterAxis > -1 && _areRightThrustersOn)
        {
            _areRightThrustersOn = false;
            SafeAction(OnStopRightThrusters);
        }

        if (Input.GetButton(FireButton))
        {
            SafeAction(OnFire);
        }

        if (Input.GetButtonDown(HyperspaceButton))
        {
            SafeAction(OnHyperspaceJump);
        }
    }

    private void OnDisable()
    {
        OnFire = null;
        OnStartLeftThrusters = null;
        OnStartMainThrusters = null;
        OnStartRightThrusters = null;
        OnStopLeftThrusters = null;
        OnStopMainThrusters = null;
        OnStopRightThrusters = null;
        OnHyperspaceJump = null;
    }
}
