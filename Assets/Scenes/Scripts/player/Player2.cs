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

    public Player upperPlayerScript; // ��̃v���C���[

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
        if (UnityEngine.Input.GetButton("Jump") || UnityEngine.Input.GetKey(KeyCode.Space)){
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

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        // �����𖞂����Ƃ���������������x��������i�������j
        if (upperPlayerScript == null || !upperPlayerScript.IsOnBlock2())
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
        }
        else
        {
            // �オBlock2�ɐG��Ă� �� �d�͖������AY���x���Z�b�g
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }

    //�Ƃ��̓����蔻��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Die(��)");
            Die();
        }
    }

    void Die()
    {
        //�����ʒu
        transform.position = new Vector3(0, -5, 0);
        rb.velocity = Vector3.zero;
    }


    public bool IsOnBlock3()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = 0.6f;
        Ray ray = new Ray(rayOrigin, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.collider.CompareTag("Block3");
        }

        return false;
    }

}