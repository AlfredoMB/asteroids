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

        _gameController = new AsteroidsGameController()
        {
            AsteroidPrefab = AsteroidPrefab,
            Camera = Camera.main
        };

        _gameController.Start();
	}
}
