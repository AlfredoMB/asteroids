using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Destructable Destructable;
    public Expirable Expirable;

    private void Awake()
    {
        Expirable.OnExpired += OnExpired;
    }

    private void OnExpired()
    {
        Destructable.ExecuteDestruction(gameObject);
    }

    private void OnEnable()
    {
        Rigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hittable = collision.gameObject.GetComponent<Hittable>();
        if (hittable == null)
        {
            return;
        }

        hittable.Hit(gameObject);
        Destructable.ExecuteDestruction();
    }
}