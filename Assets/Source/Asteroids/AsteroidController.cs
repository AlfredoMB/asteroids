using AlfredoMB.ServiceLocator;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Hittable Hittable;
    public Destructable Destructable;
    public AsteroidController FragmentAsteroid;

    private float _asteroidStartingForceIntensity;

    public void Awake()
    {
        Hittable.OnHit += OnHit;
    }

    public void Initialize(float asteroidStartingForceIntensity)
    {
        _asteroidStartingForceIntensity = asteroidStartingForceIntensity;
        Rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * asteroidStartingForceIntensity);
    }

    public void OnHit(GameObject hitter)
    {
        if (FragmentAsteroid != null)
        {
            var spawner = ServiceLocator.Get<IGameObjectSpawner>();

            var fragment = spawner.Spawn(FragmentAsteroid, transform.position, transform.rotation);
            fragment.Initialize(_asteroidStartingForceIntensity);

            fragment = spawner.Spawn(FragmentAsteroid, transform.position, transform.rotation);
            fragment.Initialize(_asteroidStartingForceIntensity);
        }

        Destructable.ExecuteDestruction(hitter);
    }
}