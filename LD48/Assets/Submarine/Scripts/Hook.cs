using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    Vector3 targetPos;

    void FixedUpdate()
    {
        UpdateRotation();
    }

    public void SetTarget(Vector3 target)
    {
        this.targetPos = target;
    }

    public void Active(bool value)
    {
        spriteRenderer.enabled = value;      
    }

    public bool IsActive()
    { 
        return spriteRenderer.enabled;
    }

    void UpdateRotation()
    {
        if (!IsActive())
        {
            return;
        }

        Vector3 dir = targetPos - transform.position;
        if ((dir.x > 0.1 || dir.x < -0.1) && (dir.y > 0.1 || dir.y < -0.1))
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
