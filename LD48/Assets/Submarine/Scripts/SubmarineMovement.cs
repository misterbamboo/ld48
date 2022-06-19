using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.SubmarineManagement.Scripts
{
    public class SubmarineMovement : MonoBehaviour
    {
        Rigidbody rb;

        [SerializeField]
        float moveSpeed;
     
        [SerializeField]
        UnityEngine.Rendering.Universal.Light2D pointLight;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if(!Game.Instance.GameOver || !Game.Instance.InGameMenu)
            {
                Move();
            }

            OutsideWaterForce();
        }

        private void Move()
        {
            if (transform.position.y > 0)
            {
                return;
            }

            var force = Vector2.zero;

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                force += new Vector2(0, moveSpeed);
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                force += new Vector2(0, -moveSpeed);
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                force += new Vector2(moveSpeed, 0);
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                force += new Vector2(-moveSpeed, 0);
            }

            rb?.AddForce(force);

            UpdateSpeed();            
        }

        private void UpdateSpeed()
        {
            if (Submarine.Instance.SpeedUpgradeBought)
            {
                moveSpeed += 2;
                Submarine.Instance.SpeedUpgradeBought = false;
            }
        }

        private void OutsideWaterForce()
        {
            if (transform.position.y > 0)
            {
                var force = new Vector2(0, -20);
                rb?.AddForce(force);
            }
        }
    }
}