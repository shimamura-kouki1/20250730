using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class test : MonoBehaviour
{
    public Image _fadePanel;
    public float _fadeDuration = 1.0f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);//オブジェクトが破棄されないようにする
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FadeOutAndLoadScene("Scene2"));
        }
    }

    public IEnumerator FadeOutAndLoadScene(string Scene2) //
    {
        _fadePanel.enabled = true; //パネルの有効化

        float elapsedTime = 0f;
        Color startColor = _fadePanel.color; // フェードパネルの開始色を取得　最初は透明＝0
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);// フェードパネルの最終色を設定　アルファ値を1に＝真っ黒

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime; //  時間経過を増やす
            _fadePanel.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);
            yield return null;
        }

        _fadePanel.color = endColor;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(Scene2);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = _fadePanel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _fadePanel.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);
            yield return null;
        }

        _fadePanel.color = endColor;
        _fadePanel.enabled = false;
    }
}