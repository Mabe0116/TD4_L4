using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map3 : MonoBehaviour
{
    public GameObject block;
    public GameObject goalBlock;

    //private List<Transform> moveBlocks = new List<Transform>();

    private Vector3 MoveBlock;

    //private Vector3 MoveBlock2;

    public TextAsset csvFile;
    int[,] map2;

    private class MovingBlock
    {
        public Transform transform;
        public Vector3 startPos;

        public MovingBlock(Transform t)
        {
            transform = t;
            startPos = t.position;
        }
    }

    private List<MovingBlock> moveBlocks = new List<MovingBlock>();


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

        MoveBlock = transform.position;
        //MoveBlock2 = transform.position;

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
                if (map2[y, x] == 4)
                {
                    if (map2[y, x] == 4)
                    {
                        GameObject obj = Instantiate(block, position, Quaternion.identity);
                        obj.tag = "MoveBlock";
                        obj.transform.localScale = new Vector3(3f, 1f, 1f);
                        moveBlocks.Add(new MovingBlock(obj.transform));
                    }

                }

                if (map2[y, x] == 5)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);
                    // 透明度を設定
                    Renderer rend = obj.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        Material mat = rend.material;
                        Color color = mat.color;
                        color.a = 0f;
                        mat.color = color;

                        mat.SetFloat("_Mode", 3);
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 0);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.EnableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = 3000;
                    }
                }
                if (map2[y, x] == 6)
                {
                    //ゴール
                    Instantiate(goalBlock, position, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin(Time.time) * 1.0f;

        foreach (MovingBlock move in moveBlocks)
        {
            Vector3 pos = move.startPos;
            move.transform.position = new Vector3(pos.x + offset, pos.y, pos.z);
        }
    }

}

