using AlfredoMB.DI;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public GameObject AsteroidPrefab;

    private AsteroidsGameController _gameController;

	private void Awake()
    {
        // startup systems
        SimpleDI.Register<IGameObjectSpawner>(new GameObjectSpawner());
        SimpleDI.Register<ICamera>(new UnityCamera());

        _gameController = new AsteroidsGameController()
        {
            AsteroidPrefab = AsteroidPrefab
        };

        _gameController.Start();
	}
}
