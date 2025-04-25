using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = rb.velocity;
        if(Input.GetKey(KeyCode.D))
        {
            v.x = moveSpeed;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            v.x = -moveSpeed;
        }
        else
        {
            v.x = 0;
        }
        rb.velocity = v;
    }
}
