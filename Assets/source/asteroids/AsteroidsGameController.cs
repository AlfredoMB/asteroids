using AlfredoMB.DI;
using UnityEngine;

public class AsteroidsGameController
{
    public GameObject AsteroidPrefab;

    private IGameObjectSpawner _spawner;
    private ICamera _camera;
    private float _forceIntensity = 500f;

    public void Start()
    {
        _spawner = SimpleDI.Get<IGameObjectSpawner>();
        _camera = SimpleDI.Get<ICamera>();

        StartStage();
    }

    private void StartStage()
    {
        InstantiateAsteroidsAroundTheScreen();
    }

    private void InstantiateAsteroidsAroundTheScreen()
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
