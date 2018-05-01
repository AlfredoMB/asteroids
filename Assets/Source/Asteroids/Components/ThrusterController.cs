using UnityEngine;

public abstract class ThrusterController : MonoBehaviour
{
    protected ShipModel _shipModel;
    protected Rigidbody _rigidBody;

    protected bool _isTurnedOn;

    public void Initialize(ShipModel shipModel, Rigidbody rigidbody)
    {
        _shipModel = shipModel;
        _rigidBody = rigidbody;
    }

    public void StartThruster()
    {
        _isTurnedOn = true;
    }

    public void StopThruster()
    {
        _isTurnedOn = false;
    }
}