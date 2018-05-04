using System;
using UnityEngine;

public abstract class BaseAsteroidsGameController : MonoBehaviour
{
    public Action OnLevelFinished;
    public Action OnShipDestroyed;
    public Action OnGameOver;

    public ShipController PlayerShip { get; protected set; }

    public abstract void Initialize();
    public abstract void StartLevel();

    public abstract ShipController CreateShip(ShipModel shipModel);
    public abstract void DestroyShip();
    public abstract void RespawnShip();

    public abstract void CreateInitialAsteroids();
    public abstract void DestroyAsteroid(AsteroidController asteroid);

    public abstract SaucerController CreateSaucer(SaucerController saucer);
    public abstract void DestroySaucer(SaucerController saucer);

    public abstract void Reset();
}