using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] DistanceJoint2D joint;
    [SerializeField] Transform firePoint;
    [SerializeField] GrappleGun grappleGun;
    private List<string> items = new List<string>();

    private float hookdistanceLastFrame = 0;
    private float pullSpeed = 3f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // when the hook is moving, rotate it facing awaiy from the submarine
        if (myRigidBody.velocity.magnitude > 0.1f)
        {
            var angle = Mathf.Atan2(transform.position.y - firePoint.position.y, transform.position.x - firePoint.position.x) * Mathf.Rad2Deg;
            spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Fire(float hookFireStrenght, float angle)
    {
        gameObject.SetActive(true);
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        var angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        myRigidBody.AddForce(angleVector * hookFireStrenght * 5, ForceMode2D.Impulse);
    }

    public void Retract()
    {
        // start coroutine pulling hook towards the submarine
        StartCoroutine(PullHook());
    }

    IEnumerator PullHook()
    {
        // while distance between hook and submarine is greater than 0.1f, pull the hook towards the submarine
        while (Vector3.Distance(transform.position, firePoint.position) > 0.5f)
        {
            var angle = Mathf.Atan2(transform.position.y - firePoint.position.y, transform.position.x - firePoint.position.x) * Mathf.Rad2Deg;
            spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            var angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            myRigidBody.AddForce(angleVector * -pullSpeed, ForceMode2D.Force);
            yield return null;
        }

        // when the hook is not pulling anymore, disable it
        gameObject.SetActive(false);
        grappleGun.Reload();
    }

    public IEnumerable<string> RetrieveItems()
    {
        return items.ToList();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hookBehaviour = other.GetComponent<IHookable>();
        if (hookBehaviour != null)
        {
            hookBehaviour.Hook(this);
            items.Add(hookBehaviour.Key);
        }
    }
}
