using System;
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
    private int _extraLives;

    public override void Initialize()
    {
        StageStateModel.Initialize(StageModel);
        CreateShip(ShipModel);
        StageStateModel.Score.OnUpdated += OnScoreUpdated;

        _extraLives = 0;
    }

    public override void StartLevel()
    {
        Reset();
        CreateInitialAsteroids();
    }

    public override void Reset()
    {
        _initialAsteroidsAmount = 0;
        _saucersSpawnedAmount = 0;
        _initialAsteroids.Clear();
        _currentAsteroids.Clear();
        _currentSaucers.Clear();
        Spawner.Reset(PlayerShip != null ? new List<GameObject>() { PlayerShip.gameObject } : null);
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
        /*
        float radius = 4.0F;
        float power = 100000.0F;
        Vector3 explosionPos = Vector3.zero;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        */
        yield return new WaitForSeconds(0.25f);
        PlayerShip.Respawn(Vector3.zero);
    }

    public override void CreateInitialAsteroids()
    {
        _initialAsteroidsAmount = Math.Min(StageModel.MaxStartingAsteroids, 
            StageModel.StartingAsteroidsAmount + (StageStateModel.Level.Value / StageModel.NumberOfLevelsToAddAsteroid));

        _initialAsteroids.Clear();
        for (int i=0; i < _initialAsteroidsAmount; i++)
        {
            _initialAsteroids.Add(CreateAsteroidAroundTheScreen(AssetLibrary.AssetSet.AsteroidPrefab));
        }
    }

    private AsteroidController CreateAsteroidAroundTheScreen(AsteroidController asteroid)
    {
        return CreateAsteroid(asteroid, GetRandomPositionAroundTheScreen(), Random.rotation, GetRandomForce());
    }

    private Vector3 GetRandomPositionAroundTheScreen()
    {
        float z = Camera.transform.position.y;
        Vector3 randomPosition = (Random.Range(0, 2) == 0)
            ? new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), z)
            : new Vector3(Random.Range(0, 2), Random.Range(0f, 1f), z);
        return Camera.ViewportToWorldPoint(randomPosition);
    }

    private Vector3 GetRandomForce()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    private AsteroidController CreateAsteroid(AsteroidController asteroid, Vector3 position, Quaternion rotation, Vector3 asteroidStartingForce)
    {
        var asteroidObject = Spawner.Spawn(asteroid.gameObject, position, Random.rotation);
        var asteroidController = asteroidObject.GetComponent<AsteroidController>();

        asteroidController.Initialize(asteroidStartingForce, Spawner, Camera);
        asteroidController.OnDestruction += OnAsteroidDestruction;

        _currentAsteroids.Add(asteroidController);
        return asteroidController;
    }

    private void OnAsteroidDestruction(GameObject destroyed, GameObject destroyer)
    {
        DestroyAsteroid(destroyed.GetComponent<AsteroidController>());
    }

    public override void DestroyAsteroid(AsteroidController asteroid)
    {
        StageStateModel.Score.Value += asteroid.AsteroidModel.DestructionScore;

        _currentAsteroids.Remove(asteroid);
        if (_initialAsteroids.Contains(asteroid))
        {
            _initialAsteroids.Remove(asteroid);
        }

        CheckForSaucerSpawn();

        if (asteroid.FragmentAsteroid != null)
        {
            CreateAsteroid(asteroid.FragmentAsteroid, asteroid.transform.position, Random.rotation, GetRandomForce());
            CreateAsteroid(asteroid.FragmentAsteroid, asteroid.transform.position, Random.rotation, GetRandomForce());
        }
        else
        {
            CheckForLevelEnd();
        }
    }

    private void CheckForSaucerSpawn()
    {
        float stageProgress = 1f - (float)_initialAsteroids.Count / _initialAsteroidsAmount;

        // check for saucer 1
        if (stageProgress > StageModel.StageProgressFor1stSaucerToAppear && _saucersSpawnedAmount == 0)
        {
            CreateSaucer(StageStateModel.Score.Value < StageModel.ScoreToSpawnOnlySmallSaucers
                ? AssetLibrary.AssetSet.BigSaucer
                : AssetLibrary.AssetSet.SmallSaucer);
        }

        // check for saucer 2
        if (stageProgress > StageModel.StageProgressFor2ndSaucerToAppear && _saucersSpawnedAmount == 1)
        {
            CreateSaucer(AssetLibrary.AssetSet.SmallSaucer);
        }
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

    public override SaucerController CreateSaucer(SaucerController saucer)
    {
        var position = GetRandomPositionAroundTheScreen();
        
        var saucerObject = Spawner.Spawn(saucer.gameObject, position);
        var saucerController = saucerObject.GetComponent<SaucerController>();

        saucerController.Initialize(PlayerShip, StageStateModel.Score, Spawner, Camera);
        saucerController.OnSaucerDestruction += OnSaucerDestruction;

        _currentSaucers.Add(saucerController);
        _saucersSpawnedAmount++;
        return saucerController;
    }

    private void OnSaucerDestruction(GameObject destroyed, GameObject destroyer)
    {
        DestroySaucer(destroyed.GetComponent<SaucerController>());
    }

    public override void DestroySaucer(SaucerController saucer)
    {
        StageStateModel.Score.Value += saucer.SaucerModel.DestructionScore;
        _currentSaucers.Remove(saucer);
    }

    private void OnScoreUpdated(int value)
    {
        if (value / StageModel.ScoreToEarnExtraLife > _extraLives)
        {
            _extraLives++;
            StageStateModel.Lives.Value++;
        }
    }
}
