using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleOperationHint : MonoBehaviour
{
    public GameObject operationHintImage;
    public float idleTime = 5.0f;   //何秒操作がなかったらヒント表示するか

    private float lastInputTime; //最後に操作があった時間
    private bool isHintVisible; //現在ヒントが表示されているか

    // Start is called before the first frame update
    void Start()
    {
        lastInputTime = Time.time;
        if (operationHintImage != null)
        {
            operationHintImage.SetActive(false);    //最初は非表示
        }
        isHintVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            lastInputTime = Time.time;
            if (isHintVisible)
            {
                HideOperationHint(); //ヒントが表示中なら非表示に
            }

        }

        //操作が一定時間なかったかどうか
        if (Time.time - lastInputTime >= idleTime)
        {
            if (!isHintVisible)
            {
                ShowOperationHint(); // ヒントが表示されていなければ表示
            }
        }
    }

    /// <summary>
    /// 操作説明を表示する
    /// </summary>
    void ShowOperationHint()
    {
        if (operationHintImage != null)
        {
            operationHintImage.SetActive(true);
            isHintVisible = true;
            Debug.Log("操作が検出されませんでした。操作説明を表示します。");
        }
    }

    /// <summary>
    /// 操作説明を非表示にする
    /// </summary>
    void HideOperationHint()
    {
        if (operationHintImage != null)
        {
            operationHintImage.SetActive(false);
            isHintVisible = false;
            Debug.Log("操作が検出されました。操作説明を非表示にします。");
        }
    }
}