using UnityEngine;

public class MovingBlockController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーの親をこのブロックに設定
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーの親子関係を解除
            other.transform.SetParent(null);
        }
    }
}
