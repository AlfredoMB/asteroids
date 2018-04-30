public class MainThrusterController : ThrusterController
{
    private void FixedUpdate()
    {
        if (_isTurnedOn)
        {
            _rigidBody.AddForce(transform.forward * _shipModel.MainThrusterStrength);
        }
    }
}