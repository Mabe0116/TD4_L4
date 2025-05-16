using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CollisionTracker : MonoBehaviour
{
    private CollisionState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<CollisionState>();
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 normal = contact.normal;

            if (normal.y > 0.5f)
                state.touchingGround = true; // 足元
            else if (normal.y < -0.5f)
                state.touchingCeiling = true; // 頭上
            else if (normal.x > 0.5f)
                state.touchingWallLeft = true; // 左側から押されてる → 右の壁
            else if (normal.x < -0.5f)
                state.touchingWallRight = true; // 右側から押されてる → 左の壁
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // すべてリセット（簡易バージョン）
        state.touchingWallRight = false;
        state.touchingWallLeft = false;
        state.touchingCeiling = false;
        state.touchingGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
