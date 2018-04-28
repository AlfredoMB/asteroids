using UnityEngine;

internal interface ICamera
{
    Transform transform { get; }

    Vector3 ViewportToWorldPoint(Vector3 position);
    Vector3 WorldToViewportPoint(Vector3 position);
}