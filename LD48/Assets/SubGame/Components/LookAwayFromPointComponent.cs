using UnityEngine;

namespace SubGame.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LookAwayFromPointComponent : MonoBehaviour
    {
        [SerializeField] private Transform lookAtPoint;
        [SerializeField] private Rigidbody2D myRigidBody;

        void Update()
        {
            // when the hook is moving, rotate it facing awaiy from the submarine
            if (myRigidBody.velocity.magnitude > 0.1f)
            {
                var angle = Mathf.Atan2(transform.position.y - lookAtPoint.position.y, transform.position.x - lookAtPoint.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
