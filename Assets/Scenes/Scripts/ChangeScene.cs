using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using System.Collections; 

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene Instance { get; private set; }
    public CanvasGroup fadePanelCanvasGroup;

    public float transitionDuration = 1.0f;
    private bool _isTransitioning = false; // シーン遷移中かどうかのフラグ

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (fadePanelCanvasGroup != null)
        {
            fadePanelCanvasGroup.alpha = 1; 
            // フェードインを開始
            StartCoroutine(Fade(fadePanelCanvasGroup, 1, 0, transitionDuration));
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!_isTransitioning)
        {
            StartCoroutine(TransitionScene(sceneName));
        }
    }

    private IEnumerator TransitionScene(string sceneName)
    {
        _isTransitioning = true;

        // フェードアウトを開始
        yield return StartCoroutine(Fade(fadePanelCanvasGroup, 0, 1, transitionDuration));
        //次のシーンをロード
        SceneManager.LoadScene(sceneName);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 新しいシーンがロードされたらフェードインする
        if (fadePanelCanvasGroup != null)
        {
            fadePanelCanvasGroup.alpha = 1;
            StartCoroutine(Fade(fadePanelCanvasGroup, 1, 0, transitionDuration));
        }
        _isTransitioning = false; 
    }


    private IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            yield return null; 
        }
        canvasGroup.alpha = endAlpha; 
    }
}