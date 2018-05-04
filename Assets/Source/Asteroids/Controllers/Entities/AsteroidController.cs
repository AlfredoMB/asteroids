using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public AsteroidModel AsteroidModel;

    public Rigidbody Rigidbody;
    public Hittable Hittable;
    public Destructable Destructable;
    public ScreenWrapper ScreenWrapper;

    public AsteroidController FragmentAsteroid;

    public Action<GameObject, GameObject> OnDestruction;

    public void Initialize(Vector3 asteroidStartingForce, BaseGameObjectSpawner spawner, BaseCamera camera)
    { 
        if (asteroidStartingForce != Vector3.zero)
        {
            Rigidbody.AddForce(asteroidStartingForce * AsteroidModel.StartSpeed);
        }

        Hittable.OnHit += OnHit;
        ScreenWrapper.Initialize(camera);
        Destructable.Initialize(spawner);
    }

    public void OnHit(GameObject hitter)
    {        
        Destructable.OnDestruction += OnDestruction;
        Destructable.ExecuteDestruction(hitter);
    }

    private void OnDisable()
    {
        OnDestruction = null;
    }
}