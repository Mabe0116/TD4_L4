using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        string sceneName = SceneManager.GetActiveScene().name;

        // gravityScaleの符号に応じてRayの方向を上下に切り替え
        Vector3 rayDirection = Vector3.down * Mathf.Sign(gravityScale);
        float distance = 0.6f;

        Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
        isBlock = Physics.Raycast(rayPosition, rayDirection, distance);

        rb.velocity = new Vector3(v.x, v.y, 0);

        // Rキーを押したときの位置リセット処理
        if (Input.GetKeyDown(KeyCode.R) && !Goal.IsGameCleared && sceneName == "Map1")
        {
            float y = gravityScale < 0 ? 0.4f : 2.4f;
            transform.position = new Vector3(2.4f, y, transform.position.z);
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.R) && !Goal.IsGameCleared && sceneName == "Map2")
        {
            float y = gravityScale < 0 ? -5f : 1.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.R) && !Goal.IsGameCleared && sceneName == "Map3")
        {
            float y = gravityScale < 0 ? -6f : -4.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.R) && !Goal.IsGameCleared && sceneName == "Map4")
        {
            float y = gravityScale < 0 ? -7f : -1.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
            rb.velocity = Vector3.zero;
        }

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