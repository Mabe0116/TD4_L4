using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRb;
    public Rigidbody shadowRb;

    public float followSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 上のプレイヤーの位置を上下反転した位置に追従
        Vector3 targetPos = new Vector3(player.position.x, -player.position.y, player.position.z);
        Vector3 newPos = Vector3.Lerp(shadowRb.position, targetPos, Time.fixedDeltaTime * followSpeed);
        shadowRb.MovePosition(newPos);

        // 上のプレイヤーの速度を上下反転してコピー（ジャンプ方向も合わせる）
        Vector3 v = playerRb.velocity;
        v.y = -v.y;
        shadowRb.velocity = v;
    }
}
