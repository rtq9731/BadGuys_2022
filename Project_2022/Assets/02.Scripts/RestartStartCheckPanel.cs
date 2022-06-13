using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RestartStartCheckPanel : MonoBehaviour
{
    [SerializeField] Text uiText = null;
    [SerializeField] Image fadeImage = null;

    bool isOn = true;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isOn)
        {
            isOn = false;
            fadeImage.DOFade(0f, 1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                UIManager.Instance.OnCutSceneOver();
            });
        }
    }

    public void SetStart(string text)
    {
        gameObject.SetActive(true);
        UIManager.Instance.OnCutScene();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        uiText.text = text;
        uiText.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
