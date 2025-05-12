using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour
{
    //接触状態を管理

    public bool touchingWallRight = false;
    public bool touchingWallLeft = false;
    public bool touchingCeiling = false;
    public bool touchingGround = false;
}
