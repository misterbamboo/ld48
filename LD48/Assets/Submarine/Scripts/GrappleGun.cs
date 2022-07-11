using Assets.Ressources;
using System.Collections;
using Assets;
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

        hook.Fire(hookFireStrenght, GetFireAngle());
    }

    void RetractHook()
    {
        state = GrappleState.Pulling;
        hook.Retract();
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

    private void UpdateLenght()
    {
        if (Submarine.Instance.HookUpgradeBought)
        {
            shootMaxDistance += 2f;
            Submarine.Instance.HookUpgradeBought = false;
        }
    }
}
