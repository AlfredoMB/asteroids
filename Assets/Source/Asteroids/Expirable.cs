using System;
using UnityEngine;

public class Expirable : MonoBehaviour
{
    public event Action OnExpired;
    public float LifeTime = 5f;

    private float _expireTime;

    public void Start()
    {
        _expireTime = Time.time + LifeTime;
    }

    public void Update()
    {
        if (Time.time < _expireTime)
        {
            return;
        }

        if (OnExpired != null)
        {
            OnExpired();
        }
        enabled = false;
    }
}