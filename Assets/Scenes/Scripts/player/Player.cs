using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 4.0f;
    public bool Cube = true;

    //public bool isTouchingBlock2 = false;
    public Player2 lowerPlayerScript; // 下のプレイヤー

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        float distance = 0.6f;
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
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
        if (UnityEngine.Input.GetButton("Jump") || UnityEngine.Input.GetKey(KeyCode.Space))
        {
            if (Cube == true)
            {
                Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
                v.y = moveJump;
            }
            else
            {
                Debug.DrawRay(rayPosition, Vector3.down * distance, Color.yellow);
            }
        }
        rb.velocity = v;
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (lowerPlayerScript != null && lowerPlayerScript.IsOnBlock3())
        {
            // 下のSphereがBlock2に触れてる → 上を浮かせる
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Yを止める
        }
        else
        {
            // 普段は落下（重力を自分でかける）
            rb.useGravity = false; // Unityの重力は使わない
            rb.AddForce(Vector3.up * -9.81f, ForceMode.Acceleration); // 自前の重力
        }

    }

    //void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Block2"))
    //    {
    //        isTouchingBlock2 = true;
    //    }
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Block2"))
    //    {
    //        isTouchingBlock2 = false;
    //    }
    //}

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

}