using UnityEngine;

[CreateAssetMenu]
public class StageModel : ScriptableObject
{
    public int StartingAsteroidsAmount = 5;
    public int NumberOfLevelsToAddAsteroid = 1;
    public int StartingLivesAmount = 3;
    public int ScoreToEarnExtraLife = 1000;
    public float StageProgressFor1stSaucerToAppear = 0.5f;
    public float StageProgressFor2ndSaucerToAppear = 0.8f;
}
