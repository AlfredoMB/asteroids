using UnityEngine;

[CreateAssetMenu]
public class AssetSet : ScriptableObject
{
    public AsteroidController AsteroidPrefab;
    public ShipController ShipPrefab;
    public SaucerController SmallSaucer;
    public SaucerController BigSaucer;
}