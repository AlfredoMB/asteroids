public class SideThrusterController : ThrusterController
{
    private void FixedUpdate()
    {
        if (_isTurnedOn)
        {
            _rigidBody.AddForceAtPosition(transform.forward * _thrusterStrength, transform.position);
        }
    }
}