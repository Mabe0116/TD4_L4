using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map2 : MonoBehaviour
{
    public GameObject block;

    public TextAsset csvFile;
    int[,] map2;

    void LoadCSV()
    {
        //CSVファイルを読み込む
        string[] lines = csvFile.text.Split('\n');
        int height = lines.Length;
        int width = lines[0].Split(',').Length;

        map2 = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            string[] lineData = lines[y].Trim().Split(',');
            for (int x = 0; x < width; x++)
            {
                int.TryParse(lineData[x], out map2[y, x]);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV();


        Vector3 position = Vector3.zero;

        //マップチップでの描画
        for (int y = 0; y < map2.GetLength(0); y++)
        {
            for (int x = 0; x < map2.GetLength(1); x++)
            {
                position.x = x;
                position.y = -y + 5;
                if (map2[y, x] == 1)
                {
                    Instantiate(block, position, Quaternion.identity);
                }
                if (map2[y, x] == 2)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);
                    obj.tag = "Block2";
                }
                if (map2[y, x] == 3)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);
                    obj.tag = "Block3";
                }

            }
        }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
