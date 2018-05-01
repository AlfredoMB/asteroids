using System;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public event Action<object> OnHit;

    public void Hit(object hitter)
    {
        if (OnHit == null)
        {
            return;
        }

        OnHit(hitter);
    }
}