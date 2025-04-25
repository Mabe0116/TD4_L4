using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject block;
    public TextAsset csvFile;
    int[,] map;

    void LoadCSV()
    {
        //CSVファイルを読み込む
        string[] lines = csvFile.text.Split('\n');
        int height = lines.Length;
        int width = lines[0].Split(',').Length;

        map = new int[height, width];

        for(int y=0;y<height;y++)
        {
            string[] lineData = lines[y].Trim().Split(',');
            for(int x=0;x<width;x++)
            {
                int.TryParse(lineData[x], out map[y, x]);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV();

        Vector3 position = Vector3.zero;

        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector3 topLeft = Camera.main.transform.position + new Vector3(-cameraWidth / 2f, cameraHeight / 2f, 0);

        //マップチップの描画
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //カメラの左上の位置から描画
                position.x = topLeft.x + x ;
                position.y = topLeft.y - y ;

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
