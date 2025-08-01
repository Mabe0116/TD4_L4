using UnityEngine;

public class MovingBlockSensor : MonoBehaviour
{
    private Transform playerOnTop = null;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.parent.position; // センサーの親（＝動くブロック）の初期位置
    }
             
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpPlayer") || other.CompareTag("BottomPlayer"))
        {
            playerOnTop = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UpPlayer") || other.CompareTag("BottomPlayer"))
        {
            if (playerOnTop == other.transform)
            {
                playerOnTop = null;
            }
        }
    }

    void LateUpdate()
    {
        if (playerOnTop != null)
        {
            // ブロックの現在の位置と前の位置の差分（＝移動量）を計算
            Vector3 blockMovement = transform.parent.position - lastPosition;

            // プレイヤーにその移動量を加算（＝一緒に動かす）
            playerOnTop.position += blockMovement;
        }

        // 毎フレームの終了時に、ブロックの位置を更新
        lastPosition = transform.parent.position;
    }
}
