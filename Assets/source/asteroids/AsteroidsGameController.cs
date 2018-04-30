using AlfredoMB.ServiceLocator;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsGameController
{
    public GameObject AsteroidPrefab;
    public GameObject ShipPrefab;

    public void Start(StageModel stage)
    {
        InstantiateShip(stage.ShipModel);
        InstantiateAsteroidsAroundTheScreen(stage);
    }

    private void InstantiateShip(ShipModel shipModel)
    {
        var ship = ServiceLocator.Get<IGameObjectSpawner>().Spawn(ShipPrefab, Vector3.zero, Quaternion.identity);
        ship.GetComponent<ShipController>().Initialize(shipModel);
    }

    private void InstantiateAsteroidsAroundTheScreen(StageModel stage)
    {
        for(int i=0; i<stage.StartingAsteroidsAmount; i++)
        {
            InstantiateAsteroidAroundTheScreen(stage);
        }
    }

    private void InstantiateAsteroidAroundTheScreen(StageModel stage)
    {
        var spawner = ServiceLocator.Get<IGameObjectSpawner>();
        var camera = ServiceLocator.Get<ICamera>();

        float z = camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        var position = camera.ViewportToWorldPoint(randomPosition);

        var asteriod = spawner.Spawn(AsteroidPrefab, position, Quaternion.identity);

        // not sure if this should be here:
        asteriod.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * stage.AsteroidStartingForceIntensity);
    }
}
