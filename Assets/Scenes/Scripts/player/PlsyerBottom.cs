using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlsyerBottom : MonoBehaviour
{
    public Transform upperPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (upperPlayer != null)
        {
            Vector3 upperPos = upperPlayer.position;
            transform.position = new Vector3(upperPos.x, -upperPos.y, upperPos.z);
        }
    }
}
   
