using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAnimations : MonoBehaviour
{
    private const float flipRight = 0f;
    private const float flipLeft = 180f;
    private const float inclineBack = 10f;

    [SerializeField] private Transform bodyToFlip;
    [SerializeField] private Transform bodyToIncline;

    private bool? shouldFaceRight;
    private bool? shouldInclineBack;

    private float wrantedYAngle;
    private float wantedIncline;

    private void Start()
    {
        wrantedYAngle = 0f;
        wantedIncline = 0f;
    }

    void Update()
    {
        if (!Game.Instance.GameOver)
        {
            AnalyseMovement();
            ComputeWantedFlip();
            ComputeWantedIncline();
        }
        else
        {
            // wrantedYAngle = 0f; (don't change flip on gameover)
            wantedIncline = 0f; // stop incline
        }

        LerpUpdateFlip();
        LerpUpdateIncline();
    }

    private void AnalyseMovement()
    {
        shouldFaceRight = null;
        shouldInclineBack = null;

        var vertical = Input.GetAxisRaw("Vertical");
        if (vertical > 0)
        {
            shouldInclineBack = true;
        }
        else if (vertical < 0)
        {
            shouldInclineBack = false;
        }

        var horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal > 0)
        {
            shouldFaceRight = true;
        }
        else if (horizontal < 0)
        {
            shouldFaceRight = false;
        }
    }

    private void ComputeWantedFlip()
    {
        if (shouldFaceRight.HasValue && shouldFaceRight.Value)
        {
            wrantedYAngle = flipRight;
        }
        else if (shouldFaceRight.HasValue && !shouldFaceRight.Value)
        {
            wrantedYAngle = flipLeft;
        }
    }

    private void LerpUpdateFlip()
    {
        var currentAngle = bodyToFlip.rotation;

        var wantedEuler = currentAngle.eulerAngles;
        wantedEuler.y = wrantedYAngle;
        var wantedAngle = Quaternion.Euler(wantedEuler);

        bodyToFlip.rotation = Quaternion.Lerp(currentAngle, wantedAngle, 4f * Time.deltaTime);
    }

    private void ComputeWantedIncline()
    {
        wantedIncline = 0;
        if (shouldInclineBack.HasValue && shouldInclineBack.Value)
        {
            wantedIncline = inclineBack;
        }
        else if (shouldInclineBack.HasValue && !shouldInclineBack.Value)
        {
            wantedIncline = -inclineBack;
        }
    }

    private void LerpUpdateIncline()
    {
        var currentAngle = bodyToIncline.rotation;

        var wantedEuler = currentAngle.eulerAngles;
        wantedEuler.z = wantedIncline;
        var wantedAngle = Quaternion.Euler(wantedEuler);

        bodyToIncline.rotation = Quaternion.Lerp(currentAngle, wantedAngle, 4f * Time.deltaTime);
    }
}
