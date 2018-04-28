using AlfredoMB.DI;
using UnityEngine;

public class AsteroidsGameController
{
    public GameObject AsteroidPrefab;
    public Camera Camera;

    private IGameObjectSpawner _spawner;

    public void Start()
    {
        _spawner = SimpleDI.Get<IGameObjectSpawner>();

        StartStage();
    }

    private void StartStage()
    {
        // instantiate asteroids around the screen
        float z = Camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);

        var position = Camera.ViewportToWorldPoint(randomPosition);

        _spawner.Spawn(AsteroidPrefab, position, Quaternion.identity);
    }
}
