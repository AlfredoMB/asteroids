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

    public override void CreateAsteroidsAroundTheScreen(int amount, float asteroidStartingForceIntensity)
    {
        for (int i=0; i<amount; i++)
        {
            CreateAsteroidAroundTheScreen(asteroidStartingForceIntensity);
        }
    }

    public override void CreateAsteroidAroundTheScreen(float asteroidStartingForceIntensity)
    {
        float z = Camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        var position = Camera.ViewportToWorldPoint(randomPosition);

        var asteriod = Spawner.Spawn(AssetLibrary.AssetSet.AsteroidPrefab.gameObject, position, Random.rotation);
        asteriod.GetComponent<AsteroidController>().Initialize(asteroidStartingForceIntensity, Spawner, Camera);
    }

    public override void Reset()
    {
        Spawner.Reset();
    }
}
