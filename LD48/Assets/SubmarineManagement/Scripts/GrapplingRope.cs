using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [SerializeField]
    Grapple grapple; 

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    int precision = 40;

    [Range(0, 20)] [SerializeField] 
    float straightenLineSpeed = 5;

    [SerializeField]
    AnimationCurve ropeAnimationCurve;

    [SerializeField]
    [Range(0.01f, 4)]
    float startWaveSize = 2;
    
    float waveSize = 0;

    [SerializeField]
    AnimationCurve ropeProgressionCurve;

    [SerializeField]
    [Range(1, 50)]
    float ropeProgressionSpeed = 1;

    [SerializeField]
    float moveTime = 0.0f;

    [SerializeField] 
    bool isGrappling = true;

    bool straightLine = true;

    void OnEnable()
    {
        moveTime = 0.0f;
        lineRenderer.positionCount = precision;
        waveSize = startWaveSize;
        straightLine = false;

        LinePointsToFirePoint();

        lineRenderer.enabled = true;
    }

    void OnDisable()
    {
        lineRenderer.enabled = false;
        isGrappling = false;
    }

    void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    public bool IsGrappling()
    {
        return isGrappling;
    }

    void DrawRope()
    {
        if (!straightLine)
        {
            var lineRendererXPos = Mathf.Round(lineRenderer.GetPosition(precision - 1).x);
            var grapplePointXPos = Mathf.Round(grapple.GetGrapplePoint().x);

            var lineRendererYPos = Mathf.Round(lineRenderer.GetPosition(precision - 1).y);
            var grapplePointYPos = Mathf.Round(grapple.GetGrapplePoint().y);

            if (lineRendererXPos == grapplePointXPos && lineRendererYPos == grapplePointYPos)
            {
                straightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {           
            if (!isGrappling)
            {               
                isGrappling = true;
                grapple.PullTarget();
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (lineRenderer.positionCount != 2) { lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            lineRenderer.SetPosition(i, grapple.GetFirePoint());
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapple.GetGrapplingDistance()).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapple.GetFirePoint(), grapple.GetGrapplePoint(), delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapple.GetFirePoint(), targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        lineRenderer.SetPosition(0, grapple.GetFirePoint());
        lineRenderer.SetPosition(1, grapple.GetGrapplePoint());
    }
}
