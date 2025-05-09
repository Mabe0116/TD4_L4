using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;

    public Transform linkedPlayer; // 下のプレイヤー
    public float rayDistance = 0.6f;

    private bool isGrounded;
    private bool isTouchingCeiling;
    private bool isTouchingWall;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // ==== 当たり判定（RayCast） ====
        Vector3 position = transform.position;

        // 足元判定（下向き）
        isGrounded = Physics.Raycast(position, Vector3.down, rayDistance) ||
                     Physics.Raycast(linkedPlayer.position, Vector3.up, rayDistance);

        // 天井判定（上向き & 下プレイヤーの下向き）
        isTouchingCeiling = Physics.Raycast(position, Vector3.up, rayDistance) ||
                            Physics.Raycast(linkedPlayer.position, Vector3.down, rayDistance);

        // 壁判定（左右向きに2方向チェック）
        bool wallRight = Physics.Raycast(position, Vector3.right, rayDistance) ||
                         Physics.Raycast(linkedPlayer.position, Vector3.right, rayDistance);
        bool wallLeft = Physics.Raycast(position, Vector3.left, rayDistance) ||
                        Physics.Raycast(linkedPlayer.position, Vector3.left, rayDistance);
        isTouchingWall = wallRight || wallLeft;

        // ==== 入力処理 ====
        Vector3 v = rb.velocity;

        // 移動（左右、壁に当たってないときのみ）
        if (!isTouchingWall)
        {
            if (Input.GetKey(KeyCode.D)) v.x = moveSpeed;
            else if (Input.GetKey(KeyCode.A)) v.x = -moveSpeed;
            else v.x = 0;
        }
        else
        {
            v.x = 0;
        }

        // ジャンプ（地面にいて、頭ぶつけてないとき）
        if ((Input.GetButton("Jump") || Input.GetKey(KeyCode.Space)) && isGrounded && !isTouchingCeiling)
        {
            v.y = moveJump;
        }

        rb.velocity = v;

        // === Debug 表示 ===
        Debug.DrawRay(position, Vector3.down * rayDistance, isGrounded ? Color.red : Color.yellow);
        Debug.DrawRay(position, Vector3.up * rayDistance, isTouchingCeiling ? Color.blue : Color.gray);
        Debug.DrawRay(position, Vector3.right * rayDistance, wallRight ? Color.green : Color.gray);
        Debug.DrawRay(position, Vector3.left * rayDistance, wallLeft ? Color.green : Color.gray);
    }
}

