using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Submarine.Scripts
{
    public class SubmarineMovement : MonoBehaviour
    {
        Rigidbody rb;

        [SerializeField]
        float moveSpeed;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
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