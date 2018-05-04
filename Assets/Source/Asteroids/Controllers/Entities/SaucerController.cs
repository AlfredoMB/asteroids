using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SaucerController : MonoBehaviour
{
    public SaucerModel SaucerModel;
    public Rigidbody Rigidbody;
    public GunController Gun;
    public Hittable Hittable;
    public Destructable Destructable;
    public ScreenWrapper ScreenWrapper;
    public ThrusterController Thruster;

    private ShipController _playerShip;
    private Observable<int> _score;
    private float _directionChangeCooldownTime;

    public event Action<GameObject, GameObject> OnSaucerDestruction;

    public void Initialize(ShipController playerShip, Observable<int> score, BaseGameObjectSpawner spawner, BaseCamera camera)
    {
        _playerShip = playerShip;
        _score = score;

        Gun.Initialize(SaucerModel.FireRate, spawner, camera);
        ScreenWrapper.Initialize(camera);
        Destructable.Initialize(spawner);
        Hittable.OnHit += DestroyOnHit;

        Thruster.Initialize(SaucerModel.MoveSpeed, Rigidbody);
        Thruster.StartThruster();
    }

    public void FireAtPlayer()
    {
        var aimAngle = (Random.value < 0.5f ? 1f : -1f) * 
            Mathf.Max(SaucerModel.StartingAimAngle - SaucerModel.AimAnglePrecisionIncreasePerScorePoint * _score.Value, 0);
        
        var playerDirection = _playerShip.transform.position - transform.position;
        Gun.transform.rotation = Quaternion.LookRotation(playerDirection) * Quaternion.AngleAxis(aimAngle, Vector3.up);
        Gun.Fire();
    }
    
    public void DestroyOnHit(GameObject hitter)
    {
        Destructable.OnDestruction += OnSaucerDestruction;
        Destructable.ExecuteDestruction(hitter);
    }

    private void OnDisable()
    {
        OnSaucerDestruction = null;
    }

    private void Update()
    {
        if (Time.time >= _directionChangeCooldownTime)
        {
            _directionChangeCooldownTime = Time.time + (1 / SaucerModel.DirectionChangeRate);
            Thruster.transform.rotation = Random.rotation;
        }

        FireAtPlayer();
    }    
}