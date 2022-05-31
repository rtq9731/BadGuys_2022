using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfoPanel : MonoBehaviour
{
    [SerializeField] Image patiantImage = null;
    [SerializeField] Image patiantImageBG = null;
    [SerializeField] Image selectBtn = null;
    [SerializeField] RectTransform offImage = null;
    [SerializeField] Text textName = null;
    [SerializeField] Text textHurtTitle = null;
    [SerializeField] Text textHurt = null;
    Text textSelectBtn = null;

    PersonInfo info = null;

    public bool canStart = false;

    Coroutine cor = null;

    private void Awake()
    {
        textSelectBtn = selectBtn.GetComponentInChildren<Text>();
    }

    public void FadeInOut(float fadeTime, Action onFade)
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(FadeCorutine(fadeTime, onFade));
    }

    public void OnPatientSelect()
    {
        GetComponent<PatientTrigger>().OnSelect(info.sceneName);
    }

    public void InitPatientInfoPanel(PersonInfo info)
    {
        this.info = info;
        patiantImage.sprite = info.personImage;
        textName.text = "ÀÇ·Ú ±â°ü : " + info.name;
        textHurt.text = info.need;
        canStart = GameManager.Instance.IsReadEmail(info.triggerEmailNum);
        offImage.gameObject.SetActive(!canStart);
    }

    private void SetRectAlpha(float alpha)
    {
        patiantImage.color = new Color(patiantImage.color.r, patiantImage.color.g, patiantImage.color.b, alpha);
        patiantImageBG.color = new Color(patiantImageBG.color.r, patiantImageBG.color.g, patiantImageBG.color.b, alpha);
        textName.color = new Color(textName.color.r, textName.color.g, textName.color.b, alpha);
        textHurt.color = new Color(textHurt.color.r, textHurt.color.g, textHurt.color.b, alpha);
        textHurtTitle.color = new Color(textHurtTitle.color.r, textHurtTitle.color.g, textHurtTitle.color.b, alpha);
        selectBtn.color = new Color(selectBtn.color.r, selectBtn.color.g, selectBtn.color.b, alpha);
        textSelectBtn.color = new Color(textSelectBtn.color.r, textSelectBtn.color.g, textSelectBtn.color.b, alpha);
    }

    private IEnumerator FadeCorutine(float fadeTime, Action onFade)
    {
        float timer = 0f;
        while (timer <= fadeTime)
        {
            timer += Time.deltaTime;
            SetRectAlpha(Mathf.Lerp(1, 0, timer / fadeTime));
            yield return null;
        }
        
        onFade();

        timer = 0f;
        while (timer <= fadeTime)
        {
            timer += Time.deltaTime;
            SetRectAlpha(Mathf.Lerp(0, 1, timer / fadeTime));
            yield return null;
        }

        yield return null;
    }
}
