using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAsteroidsGameController : MonoBehaviour
{
    public abstract GameObject CreateAsteroidAroundTheScreen(float asteroidStartingForceIntensity);
    public abstract List<GameObject> CreateAsteroidsAroundTheScreen(int amount, float asteroidStartingForceIntensity);
    public abstract ShipController CreateShip(ShipModel shipModel);
    public abstract void Reset();
}