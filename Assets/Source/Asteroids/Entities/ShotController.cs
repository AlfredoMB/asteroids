﻿using System;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public ShotModel ShotModel;

    public Rigidbody Rigidbody;
    public Destructable Destructable;
    public Expirable Expirable;
    public ScreenWrapper ScreenWrapper;

    public event Action OnTargetHit;

    public void Initialize(BaseGameObjectSpawner spawner, BaseCamera camera, int layer)
    {
        Destructable.Initialize(spawner);
        ScreenWrapper.Initialize(camera);

        Rigidbody.AddForce(transform.forward * ShotModel.Speed, ForceMode.Impulse);

        Expirable.OnExpired += OnExpired;
        
        foreach(var collider in GetComponentsInChildren<Collider>())
        {
            collider.gameObject.layer = layer;
        }
    }

    private void OnExpired()
    {
        Destructable.ExecuteDestruction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hittable = collision.gameObject.GetComponent<Hittable>();
        if (hittable == null)
        {
            return;
        }
        hittable.Hit(gameObject);

        if (OnTargetHit != null)
        {
            OnTargetHit();
        }
        Destructable.ExecuteDestruction();
    }
}