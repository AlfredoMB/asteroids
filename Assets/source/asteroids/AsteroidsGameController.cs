using System;
using AlfredoMB.Command;
using AlfredoMB.ServiceLocator;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsGameController
{
    public AsteroidController AsteroidPrefab;
    public ShipController ShipPrefab;
    public GameStateController GameStateController;

    private ShipController _playerShip;
    private GameStateController _gameState;

    public void Start(StageModel stage, GameStateController gameState)
    {
        _gameState = gameState;
        _gameState.Initialize(stage);

        InstantiateShip(stage.ShipModel);
        InstantiateAsteroidsAroundTheScreen(stage);
        ServiceLocator.Get<ICommandController>().AddListener<DestroyCommand>(OnDestroyCommand);
    }

    private void OnDestroyCommand(ICommand obj)
    {
        var destroyCommand = obj as DestroyCommand;

        if (destroyCommand.Destroyed == _playerShip.gameObject)
        {
            _gameState.UpdateLives(_gameState.GameState.CurrentLives.Value - 1);
        }
    }

    private void InstantiateShip(ShipModel shipModel)
    {
        _playerShip = ServiceLocator.Get<IGameObjectSpawner>().Spawn(ShipPrefab, Vector3.zero, Quaternion.identity);
        _playerShip.Initialize(shipModel);
    }

    private void InstantiateAsteroidsAroundTheScreen(StageModel stage)
    {
        for(int i=0; i<stage.StartingAsteroidsAmount; i++)
        {
            InstantiateAsteroidAroundTheScreen(stage);
        }
    }

    private void InstantiateAsteroidAroundTheScreen(StageModel stage)
    {
        var spawner = ServiceLocator.Get<IGameObjectSpawner>();
        var camera = ServiceLocator.Get<ICamera>();

        float z = camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        var position = camera.ViewportToWorldPoint(randomPosition);

        var asteriod = spawner.Spawn(AsteroidPrefab, position, Quaternion.identity);
        asteriod.Initialize(stage.AsteroidStartingForceIntensity);
    }
}
