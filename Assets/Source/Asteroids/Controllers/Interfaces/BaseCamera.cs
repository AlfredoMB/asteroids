using UnityEngine;

public abstract class BaseCamera : MonoBehaviour
{
    public abstract Vector3 ViewportToWorldPoint(Vector3 position);
    public abstract Vector3 WorldToViewportPoint(Vector3 position);
}