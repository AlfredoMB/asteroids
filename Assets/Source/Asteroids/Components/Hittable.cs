using System;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public event Action<GameObject> OnHit;

    public void Hit(GameObject hitter)
    {
        if (OnHit == null)
        {
            return;
        }

        OnHit(hitter);
    }

    private void OnDisable()
    {
        OnHit = null;
    }
}