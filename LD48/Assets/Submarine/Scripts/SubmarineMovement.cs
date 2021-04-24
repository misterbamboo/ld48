using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    float moveSpeed;

    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        { 
            rigidbody.AddForce(new Vector2(0, moveSpeed));        
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            rigidbody.AddForce(new Vector2(0, -moveSpeed));
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rigidbody.AddForce(new Vector2(moveSpeed, 0));
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rigidbody.AddForce(new Vector2(-moveSpeed, 0));
        }
    }
}
