using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{
    private Vector3 previousPosition;
    private List<GameObject> ridingPlayers = new List<GameObject>();

    public float amplitude = 2.0f;
    public float speedFactor = 0.5f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        previousPosition = startPos;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speedFactor) * amplitude;
        Vector3 newPosition = new Vector3(startPos.x + offset, startPos.y, startPos.z);
        Vector3 delta = newPosition - transform.position;

        transform.position = newPosition;

        foreach (GameObject player in ridingPlayers)
        {
            if (player != null)
            {
                // プレイヤーの Rigidbody がある場合は MovePosition の方が自然な動きになる
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.MovePosition(rb.position + delta);
                }
                else
                {
                    player.transform.position += delta;
                }
            }
        }

        previousPosition = transform.position;
    }

    public void AddPlayer(GameObject player)
    {
        if (!ridingPlayers.Contains(player))
        {
            ridingPlayers.Add(player);
        }
    }

    public void RemovePlayer(GameObject player)
    {
        if (ridingPlayers.Contains(player))
        {
            ridingPlayers.Remove(player);
        }
    }
}
