using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStart : MonoBehaviour
{
    public Image fadeImg;
    public float fadeTime;

    private void Awake()
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.color = new Color(0, 0, 0, 255);
    }

    private void Start()
    {
        StartCoroutine(StartFadeOut());
    }


    IEnumerator StartFadeOut()
    {
        float value = 1;
        float a = 0;
        Color col = fadeImg.color;

        while (true)
        {
            yield return null;

            value -= Time.deltaTime / fadeTime;
            a = Mathf.Clamp(value, 0, 1);
            fadeImg.color = new Color(col.r, col.g, col.b, a);

            if (a == 0) break;
        }

        fadeImg.gameObject.SetActive(false);
    }
}
