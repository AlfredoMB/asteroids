using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    public StageModel StageModel;

    public AsteroidController AsteroidPrefab;
    public ShipController ShipPrefab;
    public ShipInput Input;
    public UIController UIController;
    
    private AsteroidsGameController _gameController;

	private void Awake()
    {
        // startup systems
        ServiceLocator.Register<IGameObjectSpawner>(new GameObjectSpawner());
        ServiceLocator.Register<ICamera>(new UnityCamera());
        ServiceLocator.Register<ICommandController>(new CommandController());

        Instantiate(Input);

        var gameState = new GameStateController();

        var ui = Instantiate(UIController);
        ui.Initialize(gameState);

        _gameController = new AsteroidsGameController()
        {
            AsteroidPrefab = AsteroidPrefab,
            ShipPrefab = ShipPrefab // TODO: improve this to a more generic ship shell
        };
        
        _gameController.Start(StageModel, gameState);
	}
}
