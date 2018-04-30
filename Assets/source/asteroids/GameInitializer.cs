using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    public StageModel StageModel;

    public GameObject AsteroidPrefab;
    public GameObject ShipPrefab;
    public GameObject Input;

    private AsteroidsGameController _gameController;

	private void Awake()
    {
        // startup systems
        ServiceLocator.Register<IGameObjectSpawner>(new GameObjectSpawner());
        ServiceLocator.Register<ICamera>(new UnityCamera());
        ServiceLocator.Register<ICommandController>(new CommandController());

        GameObject.Instantiate(Input);

        _gameController = new AsteroidsGameController()
        {
            AsteroidPrefab = AsteroidPrefab,
            ShipPrefab = ShipPrefab // TODO: improve this to a more generic ship shell
        };

        _gameController.Start(StageModel);
	}
}
