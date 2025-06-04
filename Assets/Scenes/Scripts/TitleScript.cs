using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed = 5f; // �_�ő��x�i�������قǂ������j

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Map1");
        }

        //�e�L�X�g�̓_��
        if (text != null)
        {
            float alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }
    }
}
