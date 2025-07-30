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
        DontDestroyOnLoad(gameObject);//�I�u�W�F�N�g���j������Ȃ��悤�ɂ���
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
        _fadePanel.enabled = true; //�p�l���̗L����

        float elapsedTime = 0f;
        Color startColor = _fadePanel.color; // �t�F�[�h�p�l���̊J�n�F���擾�@�ŏ��͓�����0
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);// �t�F�[�h�p�l���̍ŏI�F��ݒ�@�A���t�@�l��1�Ɂ��^����

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime; //  ���Ԍo�߂𑝂₷
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