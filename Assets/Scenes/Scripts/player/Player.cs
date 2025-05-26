using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;
    public bool Cube = true;

    public Player2 lowerPlayerScript; // 下のプレイヤー
    private bool jumpRequested = false;
    private bool isJumping = false;

    void Update()
    {
        // 接地Ray
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        float distance = 0.6f;
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        Cube = Physics.Raycast(ray, distance);

        // ジャンプ判定
        bool canJumpFromGround = IsGrounded();
        bool canJumpBecauseLowerPlayerOnBlock = lowerPlayerScript != null && lowerPlayerScript.IsOnBlock3();

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
            && (canJumpFromGround || canJumpBecauseLowerPlayerOnBlock))
        {
            jumpRequested = true;
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 v = rb.velocity;

        // 横移動
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

        // ジャンプ処理
        if (jumpRequested)
        {
            v.y = moveJump;
            jumpRequested = false;
        }

        rb.velocity = v;

        // 重力制御
        if (lowerPlayerScript != null && lowerPlayerScript.IsOnBlock3())
        {
            rb.useGravity = false;

            // ジャンプ中でなければY速度を止めて浮く
            if (!isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            }
        }
        else
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }

        // 空中でジャンプ直後でも、ジャンプ中を解除
        // ただし、これは任意でタイミングを調整できます（例：Y速度が負になったら）
        if (rb.velocity.y <= 0f)
        {
            isJumping = false;
        }
    }

    public bool IsOnBlock2()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = 0.6f;
        Ray ray = new Ray(rayOrigin, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.collider.CompareTag("Block2");
        }

        return false;
    }

    public bool IsGrounded()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = 0.6f;
        Ray ray = new Ray(rayOrigin, Vector3.down);
        return Physics.Raycast(ray, rayDistance);
    }
}
