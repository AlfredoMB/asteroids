using UnityEngine;

internal class UnityCamera : ICamera
{
    public Transform transform
    {
        get
        {
            return Camera.main.transform;
        }
    }

    public Vector3 ViewportToWorldPoint(Vector3 position)
    {
        return Camera.main.ViewportToWorldPoint(position);
    }

    public Vector3 WorldToViewportPoint(Vector3 position)
    {
        return Camera.main.WorldToViewportPoint(position);
    }
}