using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] DistanceJoint2D joint;
    [SerializeField] Transform firePoint;

    private float hookdistanceLastFrame = 0;

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
}
