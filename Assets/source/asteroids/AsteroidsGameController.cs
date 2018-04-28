using AlfredoMB.DI;
using UnityEngine;

public class AsteroidsGameController
{
    public GameObject AsteroidPrefab;
    public Camera Camera;

    private IGameObjectSpawner _spawner;
    private float _forceIntensity = 100f;

    public void Start()
    {
        _spawner = SimpleDI.Get<IGameObjectSpawner>();

        StartStage();
    }

    private void StartStage()
    {
        InstantiateAsteroidsAroundTheScreen();
    }

    private void InstantiateAsteroidsAroundTheScreen()
    {
        float z = Camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);

        var position = Camera.ViewportToWorldPoint(randomPosition);

        var asteriod = _spawner.Spawn(AsteroidPrefab, position, Quaternion.identity);
        asteriod.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * _forceIntensity);
    }
}
