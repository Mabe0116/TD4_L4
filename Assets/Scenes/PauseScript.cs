using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    // ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    // ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            } else {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
    }
}
