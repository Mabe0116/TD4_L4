using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map4 : MonoBehaviour
{
    public GameObject block;
    public GameObject goalBlock;
    public GameObject spike;

    //private List<Transform> moveBlocks = new List<Transform>();

    private Vector3 MoveBlock;

    //private Vector3 MoveBlock2;

    public TextAsset csvFile;
    int[,] map2;

    private class MovingBlock
    {
        public Transform transform;
        public Vector3 startPos;
        public float amplitude;

        public MovingBlock(Transform t, float amp)
        {
            transform = t;
            startPos = t.position;
            amplitude = amp;
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
                    GameObject obj = Instantiate(block, position, Quaternion.identity);
                    obj.tag = "MoveBlock";
                    obj.transform.localScale = new Vector3(4f, 1f, 1f);

                    float amplitude = (y == 12 || y == 6) ? 6.0f : 2.0f; // ← y==12だけ振れ幅大きく
                    moveBlocks.Add(new MovingBlock(obj.transform, amplitude));

                    if (y == 11 || y == 6)
                    {
                        SetObjectTransparent(obj);
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
                if (map2[y, x] == 7)
                {
                    GameObject obj = Instantiate(spike, position, Quaternion.identity);
                    obj.tag = "Spike";

                    if (y == 10 || y == 12 || y == 13) 
                    {
                        // Z軸方向に180度回転（上下反転）
                        obj.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    }

                    Debug.Log($"Spike spawned at ({x},{y}) world pos {position}");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 
        foreach (MovingBlock move in moveBlocks)
        {
            Vector3 pos = move.startPos;
            float speedFactor = 0.5f;
            float offset = Mathf.Sin(Time.time * speedFactor) * move.amplitude; // ← それぞれの振れ幅を使用
            move.transform.position = new Vector3(pos.x + offset, pos.y, pos.z);
        }
    }

    void SetObjectTransparent(GameObject obj)
    {
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

}

