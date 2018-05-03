using UnityEngine;

public class UnityCamera : BaseCamera
{
    public Camera Camera;

    public override Vector3 ViewportToWorldPoint(Vector3 position)
    {
        return Camera.ViewportToWorldPoint(position);
    }

    public override Vector3 WorldToViewportPoint(Vector3 position)
    {
        return Camera.WorldToViewportPoint(position);
    }
}