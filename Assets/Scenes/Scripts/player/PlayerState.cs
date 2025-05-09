using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの当たり判定共有
public class PlayerState : MonoBehaviour
{
    public bool touchingWall = false;
    public bool touchingCeiling = false;
    public bool touchingGround = false;
}
