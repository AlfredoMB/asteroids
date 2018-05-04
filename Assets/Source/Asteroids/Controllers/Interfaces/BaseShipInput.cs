using System;
using UnityEngine;

public abstract class BaseShipInput : MonoBehaviour
{
    public Action OnFire;
    public Action OnStartLeftThrusters;
    public Action OnStartMainThrusters;
    public Action OnStartRightThrusters;
    public Action OnStopLeftThrusters;
    public Action OnStopMainThrusters;
    public Action OnStopRightThrusters;

    public Action OnHyperspaceJump;
}