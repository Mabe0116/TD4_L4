using UnityEngine;

public class MovingBlockSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpPlayer") || other.CompareTag("BottomPlayer"))
        {
            other.transform.SetParent(transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UpPlayer") || other.CompareTag("BottomPlayer"))
        {
            other.transform.SetParent(null);
        }
    }
}
