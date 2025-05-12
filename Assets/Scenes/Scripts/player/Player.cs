using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;

    private float horizontalInput = 0f;
    private bool jumpRequested = false;

    public CollisionState myState;         // このプレイヤーの状態
    public CollisionState linkedState;     // もう一人のプレイヤーの状態（下側）

    public Rigidbody linkedRb; // 下のプレイヤーのRigidbody
    public Transform linkedPlayer; // 下のプレイヤーのTransform

    void Update()
    {
        // 入力だけ検出
        horizontalInput = 0;

        if (Input.GetKey(KeyCode.D)) horizontalInput = 1;
        else if (Input.GetKey(KeyCode.A)) horizontalInput = -1;

        // 両方の状態を見て、接地してるかどうかチェック（どちらかが接地でOK）
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) &&
            (myState.touchingGround || linkedState.touchingCeiling) &&
               !myState.touchingCeiling &&
               !linkedState.touchingGround)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // レイキャストで当たり判定
        Vector3 rayPosition = transform.position;
        float distance = 0.6f;

        myState.touchingGround = Physics.Raycast(transform.position, Vector3.down, distance);
        myState.touchingCeiling = Physics.Raycast(transform.position, Vector3.up, distance);
        myState.touchingWallLeft = Physics.Raycast(rayPosition, Vector3.left, distance);
        myState.touchingWallRight = Physics.Raycast(rayPosition, Vector3.right, distance);

        // 下のプレイヤーの状態も同様にチェック
        Vector3 linkedRayPosition = linkedPlayer.position;
        linkedState.touchingGround = Physics.Raycast(linkedPlayer.position, Vector3.down, distance);
        linkedState.touchingCeiling = Physics.Raycast(linkedPlayer.position, Vector3.up, distance);
        linkedState.touchingWallLeft = Physics.Raycast(linkedRayPosition, Vector3.left, distance);
        linkedState.touchingWallRight = Physics.Raycast(linkedRayPosition, Vector3.right, distance);

        // 移動計算
        Vector3 move = Vector3.zero;

        if (!(myState.touchingWallRight || linkedState.touchingWallRight) && horizontalInput > 0)
        {
            move.x = horizontalInput * moveSpeed * Time.fixedDeltaTime;
        }
        else if (!(myState.touchingWallLeft || linkedState.touchingWallLeft) && horizontalInput < 0)
        {
            move.x = horizontalInput * moveSpeed * Time.fixedDeltaTime;
        }

        // 上プレイヤー移動
        rb.MovePosition(rb.position + move);

        // 下のプレイヤー：X は同じ、Y は反転
        Vector3 mirroredPos = new Vector3(
            rb.position.x + move.x,
            -rb.position.y, // Y反転
            rb.position.z
        );
        linkedRb.MovePosition(mirroredPos);
      

        // ジャンプ処理（両方に AddForce）
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * moveJump, ForceMode.Impulse);
            linkedRb.AddForce(Vector3.up * moveJump, ForceMode.Impulse);  // 同じ方向にジャンプ

            jumpRequested = false;
        }
    }

    void LateUpdate()
    {
        // 回転のリセット（見た目だけ）
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //// 下のプレイヤーの位置を上のプレイヤーに合わせて反転
        //linkedPlayer.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);

    }
}
