using AlfredoMB.Command;
using UnityEngine;

public class DestroyCommand : ICommand
{
    public GameObject Destroyed;
    public GameObject Destroyer;

    public DestroyCommand(GameObject destroyed, GameObject destroyer)
    {
        Destroyed = destroyed;
        Destroyer = destroyer;
    }
}