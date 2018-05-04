using UnityEngine;

[CreateAssetMenu]
public class SaucerModel : ScriptableObject
{
    public float FireRate;
    public float MoveSpeed;
    public float DirectionChangeRate;
    public float StartingAimAngle;
    public float AimAnglePrecisionIncreasePerScorePoint;
    public int DestructionScore;
}