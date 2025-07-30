using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenett : MonoBehaviour
{

    public Image _fadePanel;
    public float _fadeDuration;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    public IEnumerator FadeOutAndLoadScene()
    {
        _fadePanel.enabled = true; 　//パネル有効化

        float elapsedTime = 0.0f;
        Color startColor = _fadePanel.color;       // フェードパネルの開始色を取得
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // フェードパネルの最終色を設定

        // フェードアウトアニメーションを実行
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / _fadeDuration);  // フェードの進行度を計算
            _fadePanel.color = Color.Lerp(startColor, endColor, t); // パネルの色を変更してフェードアウト
            yield return null;                                     // 1フレーム待機
        }

        _fadePanel.color = endColor;  // フェードが完了したら最終色に設定


        SceneManager.LoadScene("Scene2"); // シーンをロードしてメニューシーンに遷移

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // SceneManager.sceneLoaded -= OnSceneLoaded; // イベント解除
        StartCoroutine(FadeIn()); // フェードイン開始
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color startColor = _fadePanel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _fadeDuration);
            _fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        _fadePanel.color = endColor;
        _fadePanel.enabled = false;
    }
}
