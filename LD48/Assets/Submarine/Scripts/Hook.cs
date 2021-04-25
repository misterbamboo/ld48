using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    Vector3 targetPos;

    void Update()
    {
        if (spriteRenderer.enabled)
        {
            Vector3 diff = (targetPos - transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x);
            transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
        }
    }

    public void SetTarget(Vector3 target)
    {
        this.targetPos = target;
    }

    public void Active(bool value)
    {
        spriteRenderer.enabled = value;
    }
}
