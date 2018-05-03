using UnityEngine;

public abstract class BaseAsteroidsGameController : MonoBehaviour
{
    public abstract void CreateAsteroidAroundTheScreen(float asteroidStartingForceIntensity);
    public abstract void CreateAsteroidsAroundTheScreen(int amount, float asteroidStartingForceIntensity);
    public abstract ShipController CreateShip(ShipModel shipModel);
    public abstract void Reset();
}