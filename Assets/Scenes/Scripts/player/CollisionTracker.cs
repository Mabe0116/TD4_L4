using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    private PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<PlayerState>();
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 normal = contact.normal;

            if (normal.y > 0.5f)
                state.touchingGround = true; // 足元
            else if (normal.y < -0.5f)
                state.touchingCeiling = true; // 天井
            else
                state.touchingWall = true; // 左右の壁
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // すべてリセット
        state.touchingWall = false;
        state.touchingCeiling = false;
        state.touchingGround = false;
    }
}
