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
        _fadePanel.enabled = true; �@//�p�l���L����

        float elapsedTime = 0.0f;
        Color startColor = _fadePanel.color;       // �t�F�[�h�p�l���̊J�n�F���擾
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // �t�F�[�h�p�l���̍ŏI�F��ݒ�

        // �t�F�[�h�A�E�g�A�j���[�V���������s
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // �o�ߎ��Ԃ𑝂₷
            float t = Mathf.Clamp01(elapsedTime / _fadeDuration);  // �t�F�[�h�̐i�s�x���v�Z
            _fadePanel.color = Color.Lerp(startColor, endColor, t); // �p�l���̐F��ύX���ăt�F�[�h�A�E�g
            yield return null;                                     // 1�t���[���ҋ@
        }

        _fadePanel.color = endColor;  // �t�F�[�h������������ŏI�F�ɐݒ�


        SceneManager.LoadScene("Scene2"); // �V�[�������[�h���ă��j���[�V�[���ɑJ��

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
       // SceneManager.sceneLoaded -= OnSceneLoaded; // �C�x���g����
        StartCoroutine(FadeIn()); // �t�F�[�h�C���J�n
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
