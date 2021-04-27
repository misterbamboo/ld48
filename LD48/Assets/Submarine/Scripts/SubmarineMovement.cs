using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Assets.SubmarineManagement.Scripts
{
    public class SubmarineMovement : MonoBehaviour
    {
        Rigidbody2D rb;

        [SerializeField]
        float moveSpeed;
     
        [SerializeField]
        Light2D pointLight;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
                rb.AddForce(new Vector2(0, -20));
            }
        }
    }
}