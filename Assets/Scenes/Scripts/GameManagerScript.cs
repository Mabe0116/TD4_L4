using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject block;
    int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        map = new int[,]
        {
           {1,1,1,1,1,1,1,1,1 },
           {1,0,0,0,0,0,0,0,1 },
           {1,0,0,0,0,0,0,0,1 },
           {1,0,0,0,0,0,0,0,1 },
           {1,0,0,0,0,0,0,0,1 },
           {1,1,1,1,1,1,1,1,1 },
        };

       
        Vector3 position = Vector3.zero;

        //マップチップでの描画
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                position.x = x;
                position.y = -y + 5;
                if (map[y, x] == 1)
                {
                    Instantiate(block, position, Quaternion.identity);
                }
            
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
