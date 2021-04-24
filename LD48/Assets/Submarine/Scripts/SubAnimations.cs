using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAnimations : MonoBehaviour
{
    private const float flipRight = 0f;
    private const float flipLeft = 180f;

    [SerializeField] private Transform bodyToAnimate;

    private Vector3 lastPosition;
    private bool? shouldFaceRight;
    private bool? shouldInclineBack;

    private Quaternion wrantedAngle;

    private void Start()
    {
        wrantedAngle = bodyToAnimate.rotation;
    }

    void Update()
    {
        var movement = bodyToAnimate.position - lastPosition;
        AnalyseMovement(movement);
        ComputeWantedRotation();

        ComputeWantedFlip();
        LerpUpdateFlip();

        lastPosition = bodyToAnimate.position;
    }

    private void ComputeWantedFlip()
    {
        if (shouldFaceRight.HasValue && shouldFaceRight.Value)
        {
            var current = bodyToAnimate.rotation.eulerAngles;
            current.y = flipRight;
            wrantedAngle = Quaternion.Euler(current);
        }
        else if (shouldFaceRight.HasValue && !shouldFaceRight.Value)
        {
            var current = bodyToAnimate.rotation.eulerAngles;
            current.y = flipLeft;
            wrantedAngle = Quaternion.Euler(current);
        }
    }

    private void LerpUpdateFlip()
    {
        var currentAngle = bodyToAnimate.rotation;
        bodyToAnimate.rotation = Quaternion.Lerp(currentAngle, wrantedAngle, 4f * Time.deltaTime);
    }

    private void ComputeWantedRotation()
    {
    }

    private void AnalyseMovement(Vector3 movement)
    {
        shouldFaceRight = null;
        shouldInclineBack = null;

        if (movement.x > 0)
        {
            shouldFaceRight = true;
        }
        else if (movement.x < 0)
        {
            shouldFaceRight = false;
        }

        if (movement.y > 0)
        {
            shouldInclineBack = true;
        }
        else if (movement.y < 0)
        {
            shouldInclineBack = false;
        }
    }
}
