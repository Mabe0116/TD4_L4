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
            isGameCleared = false;
            isUpPlayerInGoal = false;
            isBottomPlayerInGoal = false;


            if (Input.GetKeyDown(KeyCode.Space) || isGameCleared == true)
            {
                SceneManager.LoadScene("Map2");
            }
            if ((Input.GetKeyDown(KeyCode.Space) || isGameCleared) &&
            SceneManager.GetActiveScene().name == "Map2")
            {
                SceneManager.LoadScene("Map3");
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
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
