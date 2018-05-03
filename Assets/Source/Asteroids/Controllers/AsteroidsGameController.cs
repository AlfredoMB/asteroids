using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsGameController : BaseAsteroidsGameController
{
    public AssetLibrary AssetLibrary;
    public BaseCamera Camera;
    public BaseGameObjectSpawner Spawner;

    public StageModel StageModel;
    public ShipModel ShipModel;
    public StageStateModel StageStateModel;

    private List<AsteroidController> _initialAsteroids = new List<AsteroidController>();
    private List<AsteroidController> _currentAsteroids = new List<AsteroidController>();
    private List<SaucerController> _currentSaucers = new List<SaucerController>();
    
    private int _initialAsteroidsAmount;
    private int _saucersSpawnedAmount;

    public override void Initialize()
    {
        StageStateModel.Initialize(StageModel);
    }

    public override void StartLevel()
    {
        Reset();
        CreateShip(ShipModel);
        CreateInitialAsteroids();
    }

    public override void Reset()
    {
        _initialAsteroids.Clear();
        _currentAsteroids.Clear();
        _currentSaucers.Clear();
        Spawner.Reset();
    }

    public override ShipController CreateShip(ShipModel shipModel)
    {
        var playerShipObject = Spawner.Spawn(AssetLibrary.AssetSet.ShipPrefab.gameObject, Vector3.zero, Quaternion.identity);
        PlayerShip = playerShipObject.GetComponent<ShipController>();
        PlayerShip.Initialize(shipModel, Spawner, Camera);
        PlayerShip.OnShipDestruction += OnShipDestruction;
        return PlayerShip;
    }

    private void OnShipDestruction(GameObject destroyed, GameObject destroyer)
    {
        Debug.Log("destroyed by: " + destroyer.name, destroyer);
        DestroyShip();
    }

    public override void DestroyShip()
    {
        PlayerShip.Hide();
        if (StageStateModel.Lives.Value-- > 0)
        {
            if (OnShipDestroyed != null)
            {
                OnShipDestroyed();
            }
        }
        else if (OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public override void RespawnShip()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitUntil(() => IsSafeToRespawn());
        PlayerShip.Respawn(Vector3.zero);
    }

    private bool IsSafeToRespawn()
    {
        return true;
    }

    public override void CreateInitialAsteroids()
    {
        _initialAsteroidsAmount = StageModel.StartingAsteroidsAmount + (StageStateModel.Level.Value / StageModel.NumberOfLevelsToAddAsteroid);
        _initialAsteroids.Clear();
        for (int i=0; i < _initialAsteroidsAmount; i++)
        {
            _initialAsteroids.Add(CreateAsteroidAroundTheScreen());
        }
    }

    private AsteroidController CreateAsteroidAroundTheScreen()
    {
        float z = Camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        var position = Camera.ViewportToWorldPoint(randomPosition);

        return CreateAsteroid(position, Random.rotation, GetRandomForce(StageModel.AsteroidStartingForceIntensity));
    }

    private Vector3 GetRandomForce(float intensity)
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * intensity;
    }

    private AsteroidController CreateAsteroid(Vector3 position, Quaternion rotation, Vector3 asteroidStartingForce)
    {
        var asteroidObject = Spawner.Spawn(AssetLibrary.AssetSet.AsteroidPrefab.gameObject, position, Random.rotation);
        var asteroid = asteroidObject.GetComponent<AsteroidController>();
        asteroid.Initialize(asteroidStartingForce, Spawner, Camera);
        asteroid.OnDestruction += OnAsteroidDestruction;
        return asteroid;
    }

    private void OnAsteroidDestruction(GameObject destroyed, GameObject destroyer)
    {
        DestroyAsteroid(destroyed.GetComponent<AsteroidController>());
    }

    public override void DestroyAsteroid(AsteroidController asteroid)
    {
        StageStateModel.Score.Value += 100;

        _currentAsteroids.Remove(asteroid);

        CheckForSaucerSpawn();

        if (asteroid.FragmentAsteroid != null)
        {
            CreateAsteroid(asteroid.transform.position, Random.rotation, GetRandomForce(StageModel.AsteroidStartingForceIntensity));
            CreateAsteroid(asteroid.transform.position, Random.rotation, GetRandomForce(StageModel.AsteroidStartingForceIntensity));
        }
        else
        {
            CheckForLevelEnd();
        }
    }

    private void CheckForSaucerSpawn()
    {/*
        float stageProgress = 1f - (float)_initialAsteroids.Count / _initialAsteroidsAmount;

        // check for saucer 1
        if (stageProgress > _stageModel.StageProgressFor1stSaucerToAppear)
        {
            CreateSaucerOutsideScreen();
        }

        // check for saucer 2*/
    }

    private void CheckForLevelEnd()
    {
        if (_currentAsteroids.Count == 0)
        {
            StageStateModel.Level.Value++;
            if (OnLevelFinished != null)
            {
                OnLevelFinished();
            }
        }
    }

    public override SaucerController CreateSaucer(SaucerModel saucer)
    {
        throw new System.NotImplementedException();
    }

    public override void DestroySaucer(SaucerController saucer)
    {
        throw new System.NotImplementedException();
    }
}
