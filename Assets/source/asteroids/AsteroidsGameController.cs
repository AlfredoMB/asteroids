using System;
using AlfredoMB.DI;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsGameController
{
    public GameObject AsteroidPrefab;
    public GameObject ShipPrefab;

    private IGameObjectSpawner _spawner;
    private ICamera _camera;
    private float _forceIntensity = 500f;

    public void Start(Stage stage)
    {
        _spawner = SimpleDI.Get<IGameObjectSpawner>();
        _camera = SimpleDI.Get<ICamera>();

        StartStage(stage);
    }

    private void StartStage(Stage stage)
    {
        InstantiateShip();

        InstantiateAsteroidsAroundTheScreen(stage);
    }

    private void InstantiateShip()
    {
        _spawner.Spawn(ShipPrefab, Vector3.zero, Quaternion.identity);
    }

    private void InstantiateAsteroidsAroundTheScreen(Stage stage)
    {
        for(int i=0; i<stage.StartingAsteroidsAmount; i++)
        {
            InstantiateAsteroidAroundTheScreen();
        }
    }

    private void InstantiateAsteroidAroundTheScreen()
    {
        float z = _camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        var position = _camera.ViewportToWorldPoint(randomPosition);

        var asteriod = _spawner.Spawn(AsteroidPrefab, position, Quaternion.identity);

        // not sure if this should be here:
        asteriod.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * _forceIntensity);
    }
}
