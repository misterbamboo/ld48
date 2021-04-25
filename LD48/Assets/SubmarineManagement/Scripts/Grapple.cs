using Assets.Ressources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    GrapplingRope grapplingRope;

    [SerializeField]
    Hook hook;

    [SerializeField]
    Transform firePoint;

    Vector2 grapplePoint;
    Vector2 grapplingDistance;

    [SerializeField]
    float shootMaxDistance = 10.0f;

    float pullSpeed = 5.0f;

    bool isPulling = false;

    GameObject objectToPull;

    void Start()
    {
        ResetGrapplingRequirement();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isPulling)
        {
            SetGrapplingRequirement();

            StartHooking();
        }
        else if (Input.GetButtonUp("Fire1"))
        {            
            isPulling = true;
        }

        if (isPulling)
        {
            MoveTowardSubmarine();

            if (HaveReachSubmarine())
            {
                ResetGrapplingRequirement();
            }
        }

        MoveHookTipOfRope();
    }

    public void PullTarget()
    {
        isPulling = true;
    }

    public Vector2 GetGrapplingDistance()
    {
        return grapplingDistance;
    }

    public Vector2 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public Vector2 GetFirePoint()
    {
        return firePoint.position;
    }

    void StartHooking()
    {
        grapplingRope.enabled = true;
        hook.SetTarget(grapplePoint);
        hook.Active(true);
    }

    void MoveHookTipOfRope()
    {
        if (hook.IsActive())
        {
            hook.transform.position = grapplingRope.GetLastPosition();
        }
    }

    void MoveTowardSubmarine()
    {
        grapplePoint = Vector2.Lerp(grapplePoint, firePoint.position, Time.deltaTime * pullSpeed);

        if (objectToPull != null)
        {
            objectToPull.transform.position = grapplePoint;
        }
    }

    void ResetGrapplingRequirement()
    {
        isPulling = false;
        grapplingRope.enabled = false;
        hook.Active(false);

        objectToPull = null;
        grapplePoint = Vector3.zero;
        grapplingDistance = Vector3.zero;
    }

    bool HaveReachSubmarine()
    {
        var hookPos = hook.gameObject.transform.position;

        var distanceRemaining = Vector2.Distance(firePoint.position, hookPos);

        return distanceRemaining < 0.5 || (objectToPull != null && objectToPull.GetComponent<IRessource>().IsConsume());        
    }

    void SetGrapplingRequirement()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ressource");
        var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionVector = (clickPos - firePoint.position).normalized;

        Debug.DrawRay(firePoint.transform.position, directionVector, Color.red, 10.0f);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, directionVector, shootMaxDistance, layerMask);
        var ressource = hit.collider?.gameObject.GetComponent<IRessource>();
        if (hit.collider != null && ressource != null && !ressource.IsConsume())
        {
            objectToPull = hit.collider.gameObject;
            grapplePoint = hit.point;
        }
        else
        {
            Vector2 newPos = firePoint.transform.position + (directionVector * shootMaxDistance);
            grapplePoint = newPos;
        }

        grapplingDistance = (Vector2)firePoint.position - grapplePoint;
        grapplingRope.enabled = true;
    }
}
