using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;
    public bool Cube = true;

    public Player upperPlayerScript; // 上のプレイヤー
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.up);
        float distance = 0.6f;
        Debug.DrawRay(rayPosition, Vector3.up * distance, Color.red);
        Cube = Physics.Raycast(ray, distance);

        Vector3 v = rb.velocity;
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

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)))
        {
            bool canJumpFromGround = IsGrounded();
            bool canJumpBecauseUpperPlayerOnBlock = upperPlayerScript != null && upperPlayerScript.IsOnBlock2();

            if (canJumpFromGround || canJumpBecauseUpperPlayerOnBlock)
            {
                v.y = -moveJump;
                isJumping = true;
            }
        }

        rb.velocity = v;

    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        // 条件を満たすときだけ上向き加速度を加える（浮かす）
        if (upperPlayerScript == null || !upperPlayerScript.IsOnBlock2())
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
        }
        else
        {
            // 上がBlock2に触れてる → 重力無効化、Y速度リセット
            rb.useGravity = false;
            //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (!isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            }
        }
        isJumping = false;
    }

    public bool IsOnBlock3()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = 0.7f;
        Ray ray = new Ray(rayOrigin, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.collider.CompareTag("Block3");
        }

        return false;
    }

    public bool IsGrounded()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = 0.6f;
        Ray ray = new Ray(rayOrigin, Vector3.up);
        return Physics.Raycast(ray, rayDistance); // 何かに接地していれば true
    }

}