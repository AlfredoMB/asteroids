using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Rigidbody Rigidbody;

    private void OnEnable()
    {
        Rigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
}