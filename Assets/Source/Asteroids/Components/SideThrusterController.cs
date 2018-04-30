public class SideThrusterController : ThrusterController
{
    private void FixedUpdate()
    {
        if (_isTurnedOn)
        {
            _rigidBody.AddForceAtPosition(transform.forward * _shipModel.SideThrusterStrength, transform.position);
        }
    }
}