using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;
    public float gravityScale = 1.0f;
    private bool isBlock = true;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.useGravity = false;
    }

    void Update()
    {
        Vector3 v = rb.velocity;

        Vector3 rayPosition = transform.position;

        // gravityScaleÇÃïÑçÜÇ…âûÇ∂ÇƒRayÇÃï˚å¸Çè„â∫Ç…êÿÇËë÷Ç¶
        Vector3 rayDirection = Vector3.down * Mathf.Sign(gravityScale);
        float distance = 0.6f;

        Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
        isBlock = Physics.Raycast(rayPosition, rayDirection, distance);

        // â°à⁄ìÆ
        if (Input.GetKey(KeyCode.D))
        {
            v.x = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            v.x = -moveSpeed;
        }
        else
        {
            v.x = 0;
        }

        if (isBlock)
        {
            Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //v.y = moveJump;
                v.y = moveJump * Mathf.Sign(gravityScale);
            }
        }
        else
        {
            Debug.DrawRay(rayPosition, rayDirection * distance, Color.yellow);
        }

        rb.velocity = new Vector3(v.x, v.y, 0);
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }
}
