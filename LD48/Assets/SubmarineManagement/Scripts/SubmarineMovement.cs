using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SubmarineManagement.Scripts
{
    public class SubmarineMovement : MonoBehaviour
    {
        Rigidbody2D rb;

        [SerializeField]
        float moveSpeed;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                rb.AddForce(new Vector2(0, moveSpeed));
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                rb.AddForce(new Vector2(0, -moveSpeed));
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                rb.AddForce(new Vector2(moveSpeed, 0));
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                rb.AddForce(new Vector2(-moveSpeed, 0));
            }
        }
    }
}