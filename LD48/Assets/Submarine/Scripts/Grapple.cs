using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    GrapplingRope grapplingRope;

    [SerializeField]
    Transform firePoint;

    Vector2 grapplePoint;
    Vector2 grapplingDistance;

    [SerializeField]
    float shootMaxDistance = 100.0f;

    float pullSpeed = 5.0f;

    bool isPulling = false;

    GameObject objectToPull;

    void Start()
    {
        grapplingRope.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SetGrapplePointAndGrappleDistance();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isPulling = false;
            grapplingRope.enabled = false;
        }

        if (isPulling && grapplingRope.IsGrappling())
        {
            grapplePoint = Vector2.Lerp(objectToPull.transform.position, firePoint.position, Time.deltaTime * pullSpeed);
            objectToPull.transform.position = grapplePoint;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(grapplePoint, shootMaxDistance);
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

    private void SetGrapplePointAndGrappleDistance()
    {
        Vector2 distanceVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, distanceVector);
        if (hit.collider != null)
        {
            if (Vector2.Distance(hit.point, firePoint.position) <= shootMaxDistance)
            {
                grapplePoint = hit.point;
                grapplingDistance = grapplePoint - (Vector2)firePoint.position;

                objectToPull = hit.collider.gameObject;

                grapplingRope.enabled = true;
            }
        }       
    }
}
