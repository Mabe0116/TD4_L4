using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject block;
    public GameObject ghostBlock;
    public GameObject goalBlock;
    public TextAsset csvFile;
    int[,] map;

    void LoadCSV()
    {
        //CSV・ｽt・ｽ@・ｽC・ｽ・ｽ・ｽ・ｽﾇみ搾ｿｽ・ｽ・ｽ
        string[] lines = csvFile.text.Split('\n');
        int height = lines.Length;
        int width = lines[0].Split(',').Length;

        map = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            string[] lineData = lines[y].Trim().Split(',');
            for (int x = 0; x < width; x++)
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

        //・ｽ}・ｽb・ｽv・ｽ`・ｽb・ｽv・ｽﾌ描・ｽ・ｽ
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //・ｽJ・ｽ・ｽ・ｽ・ｽ・ｽﾌ搾ｿｽ・ｽ・ｽﾌ位置・ｽ・ｽ・ｽ・ｽ`・ｽ・ｽ
                position.x = topLeft.x + x +3.8f;
                position.y = topLeft.y - y +1.4f;

                if (map[y, x] == 1)
                {
                    Instantiate(block, position, Quaternion.identity);
                }
                else if (map[y, x] == 2)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);

                    // 透明度を設定
                    Renderer rend = obj.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        Material mat = rend.material; // インスタンスを取得
                        Color color = mat.color;
                        color.a = 0f; 
                        mat.color = color;

                        // マテリアルのレンダリングモードを透明対応に変更
                        mat.SetFloat("_Mode", 3); // 3 = Transparent
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
