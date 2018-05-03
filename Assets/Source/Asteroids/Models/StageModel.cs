using UnityEngine;

[CreateAssetMenu]
public class StageModel : ScriptableObject
{
    public int StartingAsteroidsAmount = 5;
    public int NumberOfLevelsToAddAsteroid = 1;
    public int StartingLivesAmount = 3;
    public float StageProgressFor1stSaucerToAppear = 0.5f;
    public float StagePointFor2stSaucerToAppear = 0.8f;
    public float AsteroidStartingForceIntensity = 50000f;
    public ShipModel ShipModel;
}
