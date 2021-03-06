﻿using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    public Rigidbody Rigidbody;

    private BaseCamera _camera;

    public void Initialize(BaseCamera camera)
    {
        _camera = camera;
    }

    private void FixedUpdate()
    {
        var currentPosition = transform.position;
        var viewportPosition = _camera.WorldToViewportPoint(currentPosition);

        bool isExitingLeft = viewportPosition.x < 0 && Rigidbody.velocity.x < 0;
        bool isExitingRight = viewportPosition.x > 1 && Rigidbody.velocity.x > 0;
        bool isExitingBottom = viewportPosition.y < 0 && Rigidbody.velocity.z < 0;
        bool isExitingTop = viewportPosition.y > 1 && Rigidbody.velocity.z > 0;

        if (!isExitingLeft && !isExitingRight && !isExitingBottom && !isExitingTop)
        {
            return;
        }

        if (isExitingLeft || isExitingRight)
        {
            currentPosition.Set(-currentPosition.x, currentPosition.y, currentPosition.z);
        }

        if (isExitingBottom || isExitingTop)
        {
            currentPosition.Set(currentPosition.x, currentPosition.y, -currentPosition.z);
        }

        Rigidbody.MovePosition(currentPosition);
    }
}
