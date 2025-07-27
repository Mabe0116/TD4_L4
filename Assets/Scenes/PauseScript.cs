using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    // ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    // ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    public Button yesButton;
    public Button noButton;

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

                // ボタンを子オブジェクトから探す
                yesButton = pauseUIInstance.transform.Find("YesButton").GetComponent<Button>();
                noButton = pauseUIInstance.transform.Find("NoButton").GetComponent<Button>();
            } else {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }

        string nextSceneName = "Title";

        // pause中のみA/Dキーを受け付ける
        if (pauseUIInstance != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                yesButton.onClick.Invoke();

                Time.timeScale = 1f;  
                ChangeScene.Instance.LoadScene(nextSceneName);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                noButton.onClick.Invoke();
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
    }
}
