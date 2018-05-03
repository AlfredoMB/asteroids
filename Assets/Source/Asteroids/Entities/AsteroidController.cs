using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Hittable Hittable;
    public Destructable Destructable;
    public ScreenWrapper ScreenWrapper;

    public AsteroidController FragmentAsteroid;

    public Action<GameObject> OnDestruction;

    private float _asteroidStartingForceIntensity;
    private BaseGameObjectSpawner _spawner;
    private BaseCamera _camera;

    public void Initialize(float asteroidStartingForceIntensity, BaseGameObjectSpawner spawner, BaseCamera camera)
    {
        _spawner = spawner;
        _camera = camera;
        _asteroidStartingForceIntensity = asteroidStartingForceIntensity;

        Rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * asteroidStartingForceIntensity);

        Hittable.OnHit += OnHit;
        ScreenWrapper.Initialize(camera);
        Destructable.Initialize(spawner);
    }

    public void OnHit(GameObject hitter)
    {
        if (FragmentAsteroid != null)
        {
            var fragment = _spawner.Spawn(FragmentAsteroid.gameObject, transform.position, transform.rotation);
            fragment.GetComponent<AsteroidController>().Initialize(_asteroidStartingForceIntensity, _spawner, _camera);

            fragment = _spawner.Spawn(FragmentAsteroid.gameObject, transform.position, transform.rotation);
            fragment.GetComponent<AsteroidController>().Initialize(_asteroidStartingForceIntensity, _spawner, _camera);
        }
        
        Destructable.OnDestruction += OnDestruction;
        Destructable.ExecuteDestruction(hitter);
    }
}