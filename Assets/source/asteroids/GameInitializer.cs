using AlfredoMB.Command;
using AlfredoMB.DI;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public GameObject ShipPrefab;
    public GameObject Input;

    private AsteroidsGameController _gameController;

	private void Awake()
    {
        // startup systems
        SimpleDI.Register<IGameObjectSpawner>(new GameObjectSpawner());
        SimpleDI.Register<ICamera>(new UnityCamera());
        SimpleDI.Register<ICommandController>(new CommandController());

        GameObject.Instantiate(Input);

        _gameController = new AsteroidsGameController()
        {
            AsteroidPrefab = AsteroidPrefab,
            ShipPrefab = ShipPrefab
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
