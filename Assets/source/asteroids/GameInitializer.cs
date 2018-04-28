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

        var stage = new Stage()
        {
            StartingLivesAmount = 3,
            StartingAsteroidsAmount = 5,
            StagePointFor1stSaucerToAppear = 0.5f,
            StagePointFor2stSaucerToAppear = 0.8f
        };

        _gameController.Start(stage);
	}
}
