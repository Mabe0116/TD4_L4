using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : MonoBehaviour
{
    public GameObject clearUI;

    private bool isGameCleared = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR    
        // 時間が止まっていて、ゲームがクリア済み、かつ Space キーを押したら再開
        if (isGameCleared && Time.timeScale == 0f && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            clearUI.SetActive(false); // UIを消したい場合
            isGameCleared = false;
        }
#endif
    }

    void OnTriggerEnter(Collider other)
    {
        // 接触したオブジェクトの名前で判定
        if (other.gameObject.name == "Player")
        {
            clearUI.SetActive(true);
            Time.timeScale = 0f;
            isGameCleared = true;
        }
    }
}
