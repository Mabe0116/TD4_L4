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

    private Animator animator;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.useGravity = false;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 v = rb.velocity;

        Vector3 rayPosition = transform.position;

        string sceneName = SceneManager.GetActiveScene().name;

        // gravityScale�̕����ɉ�����Ray�̕������㉺�ɐ؂�ւ�
        Vector3 rayDirection = Vector3.down * Mathf.Sign(gravityScale);
        float distance = 0.6f;

        Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
        isBlock = Physics.Raycast(rayPosition, rayDirection, distance);

        rb.velocity = new Vector3(v.x, v.y, 0);



        // R�L�[���������Ƃ��̈ʒu���Z�b�g����
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

        //���݂̊p�x�̎擾
        Vector3 currentEulerAngles = transform.rotation.eulerAngles;
        float targetYRotation = currentEulerAngles.y;

        // ���ړ�
        if (Input.GetKey(KeyCode.D))
        {
            v.x = moveSpeed;

            targetYRotation = 110;
            animator.SetBool("Walk", true);
            animator.SetBool("Idle", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            v.x = -moveSpeed;

            targetYRotation = 220;
            animator.SetBool("Walk", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            v.x = 0;
        }

        //y���W�̂ݕύX
        transform.rotation = Quaternion.Euler(currentEulerAngles.x, targetYRotation, currentEulerAngles.z);


        if (isBlock)
        {
            if (animator != null)
            {
                animator.SetBool("Jump", false); // �n�ʂɂ���̂ŁA�W�����v�A�j���[�V�������~
            }

            Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //v.y = moveJump;
                v.y = moveJump * Mathf.Sign(gravityScale);

                if (animator != null)
                {
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Jump", true);
                }
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