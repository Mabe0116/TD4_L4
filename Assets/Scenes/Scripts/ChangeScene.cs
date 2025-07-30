using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using System.Collections; 

public class ChangeScene : MonoBehaviour
{
    // シングルトンパターンでどこからでもアクセスできるようにする
    public static ChangeScene Instance { get; private set; }
    public CanvasGroup fadePanelCanvasGroup;

    public float transitionDuration = 1.0f;

    private bool _isTransitioning = false; // シーン遷移中かどうかのフラグ

    void Awake()
    {
        // シングルトンパターン: 複数のインスタンスが生成されないようにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが切り替わってもこのGameObjectは破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は自分自身を破棄
        }
    }

    void Start()
    {
        // ゲーム開始時（または最初のシーンロード時）にフェードインする
        if (fadePanelCanvasGroup != null)
        {
            fadePanelCanvasGroup.alpha = 1; // 最初は完全に不透明
            // フェードイン（シーンが見えるようになる）を開始
            StartCoroutine(Fade(fadePanelCanvasGroup, 1, 0, transitionDuration));
        }
    }

    /// <param name="sceneName">遷移先のシーン名</param>
    public void LoadScene(string sceneName)
    {
        if (!_isTransitioning)
        {
            StartCoroutine(TransitionScene(sceneName));
        }
    }

    /// <param name="sceneName">遷移先のシーン名</param>
    private IEnumerator TransitionScene(string sceneName)
    {
        _isTransitioning = true;

        // フェードアウトを開始
        yield return StartCoroutine(Fade(fadePanelCanvasGroup, 0, 1, transitionDuration));

        // フェードアウトが完了したら次のシーンをロード
        SceneManager.LoadScene(sceneName);
    }

    // シーンがロードされた後に自動的に呼ばれるメソッド
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
            // まず不透明にしてからフェードインさせる
            fadePanelCanvasGroup.alpha = 1;
            StartCoroutine(Fade(fadePanelCanvasGroup, 1, 0, transitionDuration));
        }
        _isTransitioning = false; // 遷移フラグをリセット
    }


    private IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            yield return null; // 1フレーム待つ
        }
        canvasGroup.alpha = endAlpha; // 確実に目標のAlpha値にする
    }
}