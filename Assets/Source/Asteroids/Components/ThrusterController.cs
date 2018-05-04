using UnityEngine;

public abstract class ThrusterController : MonoBehaviour
{
    protected float _thrusterStrength;
    protected Rigidbody _rigidBody;

    protected bool _isTurnedOn;

    public void Initialize(float thrusterStrength, Rigidbody rigidbody)
    {
        _thrusterStrength = thrusterStrength;
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