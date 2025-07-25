using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Goal : MonoBehaviour
{
    public GameObject clearUI;
    private static GameObject spawnedClearUI;
    private static bool isGameCleared = false;

    public static bool IsGameCleared => isGameCleared;

    private static bool isUpPlayerInGoal = false;
    private static bool isBottomPlayerInGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameCleared = false;
        isUpPlayerInGoal = false;
        isBottomPlayerInGoal = false;
        
        if (spawnedClearUI!=null)
        {
            Destroy(spawnedClearUI);
            spawnedClearUI = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
   
        // 時間が止まっていて、ゲームがクリア済み、かつ Space キーを押したら再開
        if (isGameCleared && Time.timeScale == 0f && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;

            if (spawnedClearUI != null)
            {
                Destroy(spawnedClearUI);
                spawnedClearUI = null;
            }
           // isGameCleared = false;
            isUpPlayerInGoal = false;
            isBottomPlayerInGoal = false;


            string currentSceneName = SceneManager.GetActiveScene().name;
            string nextSceneName = "";

            if (currentSceneName == "Map1")
            {
                nextSceneName = "Map2";
            }
            else if (currentSceneName == "Map2")
            {
                nextSceneName = "Map3";
            }
            else if (currentSceneName == "Map3")
            {
                nextSceneName = "Map4";
            }
            else if (currentSceneName == "Map4")
            {
                // 例えば、Map4が最終ステージならタイトルに戻るなど
                nextSceneName = "Map1"; // 例: 最後のシーンからタイトルへ
                Debug.Log("Game Cleared! Returning to Title Scene.");
            }

            if (!string.IsNullOrEmpty(nextSceneName))
            {
               
                ChangeScene.Instance.LoadScene(nextSceneName);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isGameCleared)
        {
            return;
        }

        if (other.CompareTag("UpPlayer"))
        {
            isUpPlayerInGoal = true;
        }
        else if (other.CompareTag("BottomPlayer"))
        {
            isBottomPlayerInGoal = true;
        }

        //二つゴールに入っていたら
        if (isUpPlayerInGoal && isBottomPlayerInGoal && !isGameCleared)
        {
            if (spawnedClearUI == null)
            {
                spawnedClearUI = Instantiate(clearUI);
                spawnedClearUI.SetActive(true);
            }

            Time.timeScale = 0f;
            isGameCleared = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 既にゲームがクリア状態であれば、それ以上の処理はしない
        if (isGameCleared)
        {
            return;
        }

        if (other.CompareTag("UpPlayer"))
        {
            isUpPlayerInGoal = false;
            //Debug.Log("isUpPlayerHitGoal");
        }
        else if (other.CompareTag("BottomPlayer"))
        {
            isBottomPlayerInGoal = false;
            //Debug.Log("isBottomPlayerHitGoal");
        }
    }

   
}
