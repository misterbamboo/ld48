using System.Collections;
using UnityEngine;

public enum GrappleState
{
    Idle,
    Grappling,
    Pulling,
}

public class GrappleGun : MonoBehaviour
{
    [SerializeField] GrappleSound grappleSound;
    [SerializeField] GrapplingRope grapplingRope;
    [SerializeField] Hook hook;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootMaxDistance = 10.0f;
    private GrappleState state = GrappleState.Idle;
    private float hookFireStrenght = 1f;
    float pullSpeed = 5.0f;
    bool isPulling = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (state == GrappleState.Idle)
            {
                FireHook();

            }
            else if (state == GrappleState.Grappling)
            {
                RetractHook();
            }
        }
    }

    void FireHook()
    {
        state = GrappleState.Grappling;
        grappleSound.PlayRandomSound();

        var angle = GetFireAngle();
        hook.gameObject.SetActive(true);

        var angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        hook.GetComponent<Rigidbody2D>().AddForce(angleVector * hookFireStrenght * 5, ForceMode2D.Impulse);
    }

    public void RetractHook()
    {
        state = GrappleState.Pulling;
        StartCoroutine(RetractHookCoroutine());
    }

    IEnumerator RetractHookCoroutine()
    {
        var renderer = hook.GetComponent<SpriteRenderer>();
        var rigidBody = hook.GetComponent<Rigidbody2D>();
        var hookTransform = hook.GetComponent<Transform>();

        while (Vector3.Distance(hookTransform.position, firePoint.position) > 0.5f)
        {
            var angle = Mathf.Atan2(hookTransform.position.y - firePoint.position.y, hookTransform.position.x - firePoint.position.x) * Mathf.Rad2Deg;

            var angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            rigidBody.AddForce(angleVector * -pullSpeed, ForceMode2D.Force);
            yield return null;
        }

        hook.gameObject.SetActive(false);
        Reload();
    }

    public void Reload()
    {
        state = GrappleState.Idle;
    }

    private float GetFireAngle()
    {
        var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x) * Mathf.Rad2Deg;
    }
}
