using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsGameController : BaseAsteroidsGameController
{
    public AssetLibrary AssetLibrary;
    public BaseCamera Camera;
    public BaseGameObjectSpawner Spawner;
    
    public override ShipController CreateShip(ShipModel shipModel)
    {
        var playerShipObject = Spawner.Spawn(AssetLibrary.AssetSet.ShipPrefab.gameObject, Vector3.zero, Quaternion.identity);
        var playerShip = playerShipObject.GetComponent<ShipController>();
        playerShip.Initialize(shipModel, Spawner, Camera);
        return playerShip;
    }

    public override List<GameObject> CreateAsteroidsAroundTheScreen(int amount, float asteroidStartingForceIntensity)
    {
        var asteroids = new List<GameObject>();
        for (int i=0; i<amount; i++)
        {
            var asteroid = CreateAsteroidAroundTheScreen(asteroidStartingForceIntensity);
            asteroids.Add(asteroid);
        }
        return asteroids;
    }

    public override GameObject CreateAsteroidAroundTheScreen(float asteroidStartingForceIntensity)
    {
        float z = Camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        var position = Camera.ViewportToWorldPoint(randomPosition);

        var asteroid = Spawner.Spawn(AssetLibrary.AssetSet.AsteroidPrefab.gameObject, position, Random.rotation);
        asteroid.GetComponent<AsteroidController>().Initialize(asteroidStartingForceIntensity, Spawner, Camera);
        return asteroid;
    }

    public override void Reset()
    {
        Spawner.Reset();
    }
}
