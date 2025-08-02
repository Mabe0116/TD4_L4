using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float moveJump = 5.0f;
    public float gravityScale = 1.0f;
    public GameObject bombParticle;
    public Player otherPlayer;

    private Animator animator;
    private AudioSource audioSource;
    private bool isBlock = true;
    public bool canControl = true; // 他スクリプトからもアクセスできるように public に
    private ParticleSystem dustParticle;


    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.useGravity = false;
        animator = GetComponent<Animator>();
        dustParticle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();


        // シーンがタイトルシーンであれば、初期化時にアニメーションをIdleに設定
        if (SceneManager.GetActiveScene().name == "title")
        {
            if (animator != null)
            {
                animator.Play("Walk", 0, 0f); // Idleステートの最初から再生
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Jump", false);
                animator.SetBool("fall", false);
                // タイトルシーンではAnimatorを無効化することで、
                // 他のアニメーション遷移を防ぐ
                animator.enabled = false;
            }
            // タイトルシーンではプレイヤーの物理演算も無効化する（必要であれば）
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }

    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Title")
        {
            return;
        }

            Vector3 v = rb.velocity;
        if (!canControl)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        //Vector3 v = rb.velocity;
        Vector3 rayPosition = transform.position;
        string sceneName = SceneManager.GetActiveScene().name;

        Vector3 rayDirection = Vector3.down * Mathf.Sign(gravityScale);
        float distance = 0.6f;
        Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
        isBlock = Physics.Raycast(rayPosition, rayDirection, distance);

        rb.velocity = new Vector3(v.x, v.y, 0);

        // Rキーでリセット
        if (Input.GetKeyDown(KeyCode.R) && !Goal.IsGameCleared)
        {
            StartCoroutine(RespawnAfterDelay(0.5f)); // ← コルーチンを開始
        }

        // 横移動
        Vector3 currentEulerAngles = transform.rotation.eulerAngles;
        float targetYRotation = currentEulerAngles.y;


        // 横移動
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

        transform.rotation = Quaternion.Euler(currentEulerAngles.x, targetYRotation, currentEulerAngles.z);

        // ジャンプ処理
        if (isBlock)
        {
            if (animator != null)
            {
                animator.SetBool("Jump", false); // 地面にいるので、ジャンプアニメーションを停止
                animator.SetBool("fall", false);
            }
                Debug.DrawRay(rayPosition, rayDirection * distance, Color.red);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                v.y = moveJump * Mathf.Sign(gravityScale);

                if (animator != null)
                {
                    animator.Play("Jump", 0, 0f);

                    animator.SetBool("Idle", false);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Jump", true);
                }
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }

            }
        }
        else
        {
            Debug.DrawRay(rayPosition, rayDirection * distance, Color.yellow);

            if (animator != null)
            {
                animator.SetBool("fall", true);
            }
        }

        //足の軌跡のパーティクル制御
        var emission = dustParticle.emission;
        if (isBlock && animator.GetBool("Walk")) 
        {
            if (!emission.enabled)
            {
                emission.enabled = true; // パーティクル実行
            }
        }
        else 
        {
            if (emission.enabled)
            {
                emission.enabled = false; // パーティクルを止める
            }
        }


        rb.velocity = new Vector3(v.x, v.y, 0);
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Spike")
        {
            StartCoroutine(RespawnBothPlayers(0.5f));
        }
    }

    private IEnumerator RespawnBothPlayers(float delay)
    {
        canControl = false;
        rb.velocity = Vector3.zero;
        Instantiate(bombParticle, transform.position, Quaternion.identity);

        if (otherPlayer != null)
        {
            otherPlayer.canControl = false;
            otherPlayer.rb.velocity = Vector3.zero;
            Instantiate(bombParticle, otherPlayer.transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(delay);

        RespawnToStart();
        if (otherPlayer != null)
        {
            otherPlayer.RespawnToStart();
            otherPlayer.canControl = true;
        }

        canControl = true;
    }

    public void RespawnToStart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        float y = 0;

        // 現在位置で爆発パーティクルを出す
        Instantiate(bombParticle, transform.position, Quaternion.identity);

        if (sceneName == "Map1")
        {
            y = gravityScale < 0 ? 0.4f : 2.4f;
            transform.position = new Vector3(3.0f, y, transform.position.z); // ← X座標を右に（例：3.0f）
        }
        else if (sceneName == "Map2")
        {
            y = gravityScale < 0 ? -5f : 1.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
        }
        else if (sceneName == "Map3")
        {
            y = gravityScale < 0 ? -6f : -4.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
        }
        else if (sceneName == "Map4")
        {
            y = gravityScale < 0 ? -7f : -1.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
        }
        else if (sceneName == "Map5")
        {
            y = gravityScale < 0 ? -7f : -1.0f;
            transform.position = new Vector3(1.0f, y, transform.position.z);
        }

        rb.velocity = Vector3.zero;
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        canControl = false;
        rb.velocity = Vector3.zero;

        Instantiate(bombParticle, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(delay);

        RespawnToStart();
        canControl = true;
    }

}
