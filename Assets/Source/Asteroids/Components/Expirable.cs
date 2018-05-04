using System;
using UnityEngine;

public class Expirable : MonoBehaviour
{
    public event Action OnExpired;
    public float LifeTime = 5f;

    private float _expireTime;
    private bool _isExpired;

    public void OnEnable()
    {
        _isExpired = false;
        _expireTime = Time.time + LifeTime;
    }

    public void Update()
    {
        if (_isExpired)
        {
            return;
        }

        if (Time.time < _expireTime)
        {
            return;
        }

        if (OnExpired != null)
        {
            OnExpired();
        }
        _isExpired = true;
    }
}