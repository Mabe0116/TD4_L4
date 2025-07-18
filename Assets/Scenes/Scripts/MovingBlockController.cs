using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{
    private List<GameObject> ridingPlayers = new List<GameObject>();
    public float amplitude = 2.0f;
    public float speedFactor = 0.5f;

    private Vector3 startPos;
    private float startTime;

    void Awake()
    {
        startPos = transform.position;
    }

    void Start()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        float elapsed = Time.timeSinceLevelLoad - startTime;
        float offset = Mathf.Sin(elapsed * speedFactor) * amplitude;
        Vector3 newPosition = startPos + new Vector3(offset, 0, 0);
        Vector3 delta = newPosition - transform.position;

        transform.position = newPosition;

        foreach (GameObject player in ridingPlayers)
        {
            if (player != null)
            {
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

    public void ResetStartPosition()
    {
        startPos = transform.position;
        startTime = Time.timeSinceLevelLoad;
    }
}
