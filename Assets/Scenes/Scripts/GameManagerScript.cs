using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject block;
    public GameObject spike;

    public TextAsset csvFile;
    int[,] map;

    void LoadCSV()
    {
        //CSV�t�@�C����ǂݍ���
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

        //�}�b�v�`�b�v�ł̕`��
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
                else if (map[y, x] == 2)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);

                    // �����x��ݒ�
                    Renderer rend = obj.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        Material mat = rend.material; // �C���X�^���X���擾
                        Color color = mat.color;
                        color.a = 0f; 
                        mat.color = color;

                        // �}�e���A���̃����_�����O���[�h�𓧖��Ή��ɕύX
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
                if (map[y, x] == 4)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);
                    obj.tag = "MoveBlock";
                }
                if(map[y, x] == 5)
                {
                    GameObject obj = Instantiate(block, position, Quaternion.identity);
                    obj.tag = "MoveBlock2";
                }
                if (map[y, x] == 6)
                {
                    GameObject obj = Instantiate(spike, position, Quaternion.identity);
                    obj.tag = "Spike";
                    Debug.Log($"Spike spawned at ({x},{y}) world pos {position}");
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
