using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;

    private bool isGrounded = false;
    private float horizontalInput = 0f;
    private bool jumpRequested = false;

    void Update()
    {
        // 入力だけ検出
        horizontalInput = 0;

        if (Input.GetKey(KeyCode.D)) horizontalInput = 1;
        else if (Input.GetKey(KeyCode.A)) horizontalInput = -1;

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // 接地判定（レイキャスト）
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        float distance = 0.6f;
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        isGrounded = Physics.Raycast(ray, distance);

        // 移動処理（MovePositionを使う）
        Vector3 move = new Vector3(horizontalInput * moveSpeed * Time.fixedDeltaTime, 0f, 0f);
        rb.MovePosition(rb.position + move);

        // ジャンプ処理（AddForce）
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * moveJump, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    void LateUpdate()
    {
        // 回転のリセット（見た目だけ）
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
