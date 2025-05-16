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

        rb.useGravity = false;  // UnityÇÃèdóÕÇñ≥å¯âª
        rb.AddForce(Vector3.up * 1.5f, ForceMode.Acceleration);

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
        if (UnityEngine.Input.GetButton("Jump") || UnityEngine.Input.GetKey(KeyCode.Space))        {
            if (Cube == true)
            {
                Debug.DrawRay(rayPosition, Vector3.up * distance, Color.red);
                v.y = -moveJump;
            }
            else
            {
                Debug.DrawRay(rayPosition, Vector3.up * distance, Color.yellow);
            }
        }
        rb.velocity = v;
    }
}