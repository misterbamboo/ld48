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
            grapplingRope.enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(grapplePoint, shootMaxDistance);
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
                grapplingRope.enabled = true;
            }
        }
    }
}
