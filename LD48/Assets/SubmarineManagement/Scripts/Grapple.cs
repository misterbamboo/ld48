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
    Camera mainCamera;

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
        grapplingRope.enabled = false;
        hook.Active(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SetGrapplingRequirement();

            if (objectToPull != null)
            {
                grapplingRope.enabled = true;
                hook.SetTarget(grapplePoint);
                hook.Active(true);
            }    
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            ResetGrapplingRequirement();
        }

        if (isPulling && grapplingRope.IsGrappling() && objectToPull != null)
        {
            grapplePoint = Vector2.Lerp(objectToPull.transform.position, firePoint.position, Time.deltaTime * pullSpeed);
            objectToPull.transform.position = grapplePoint;
            hook.transform.position = grapplePoint;

            if (HaveReachSubmarine())
            {
                ResetGrapplingRequirement();
            }
        }
        else if(objectToPull != null)
        {
            hook.transform.position = grapplingRope.GetLastPosition();
        }
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

    private void ResetGrapplingRequirement()
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
        return objectToPull == null;
    }

    private void SetGrapplingRequirement()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ressource");

        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 directionVector = (new Vector3(ray.x, ray.y, ray.z) - firePoint.position).normalized;


        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, directionVector, shootMaxDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawRay(firePoint.transform.position, directionVector, Color.green, 10.0f);

            if (Vector2.Distance(hit.point, firePoint.position) <= shootMaxDistance)
            {
                grapplePoint = hit.point;
                grapplingDistance = grapplePoint - (Vector2)firePoint.position;

                grapplingRope.enabled = true;
            }
        }
        else
        {
            Debug.DrawRay(firePoint.transform.position, directionVector, Color.red, 10.0f);
        }

        /*
        Vector3 directionVector = mainCamera.ScreenToWorldPoint(Input.mousePosition - firePoint.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, directionVector, shootMaxDistance, layerMask);
                
        if (hit.collider != null)
        {
            Debug.DrawRay(firePoint.transform.position, directionVector, Color.green, 10.0f);
            
            if(Vector2.Distance(hit.point, firePoint.position) <= shootMaxDistance)
            {
                grapplePoint = hit.point;
                grapplingDistance = grapplePoint - (Vector2) firePoint.position;

                grapplingRope.enabled = true;
            }
        }
        else
        {
            Debug.DrawRay(firePoint.transform.position, directionVector, Color.red, 10.0f);
        }
        */
    }
}
