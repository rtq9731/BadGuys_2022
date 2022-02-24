using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StateMentOpinionManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> movePos;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private float movingTime = 3f;
    [SerializeField]
    private float typingSpeed = 2f;

    private int stepNum;

    public List<Text> texts;

    private void Start()
    {
        MoveStep(0);
        stepNum = 0;
        StartCoroutine(TextClear());

        FindTextAndSet("Patient_name", "ÃµÁø¸¸");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            stepNum--;
            if (stepNum < 0) stepNum = 0;
            MoveStep(stepNum);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            stepNum++;
            if (stepNum > movePos.Count - 1) stepNum = movePos.Count - 1;
            MoveStep(stepNum);
        }
    }

    public void FindTextAndSet(string textName, string textContent)
    {
        Text target = texts.Find(item => item.transform.name == textName);

        if (target == null)
        {
            target = transform.Find(textName).GetComponent<Text>();
        }

        StartCoroutine(TextTyping(target, textContent));
        Debug.Log(textName + " = " + textContent);
    }

    public void MoveStep(int num)
    {
        paper.transform.DOMove(movePos[num].transform.position, movingTime);
        Debug.Log("paper tranform = " + movePos[num].transform.position);
    }

    public IEnumerator TextClear()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].text = "";
        }

        yield return new WaitForSeconds(0f);
    }

    private IEnumerator TextTyping(Text targetText, string content)
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i <= content.Length; i++)
        {
            targetText.text = content.Substring(0, i);

            float delay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(delay);
        }

    }

    //public void SetPlayerName(string name)
    //{
    //    Text playerName = texts.Find(item => item.transform.name == "Player_name");

    //    if (playerName == null) playerName = transform.Find("Player_name").GetComponent<Text>();

    //    playerName.text = name;
    //    Debug.Log("playerName = " + name);
    //}

    //public void SetPatientName(string name)
    //{
    //    Text patientName = texts.Find(item => item.transform.name == "Patient_name");

    //    if (patientName == null) patientName = transform.Find("Patient_name").GetComponent<Text>();

    //    patientName.text = name;
    //    Debug.Log("patientName = " + name);
    //}

    //public void SetPatientSocialSecurityNumber(string name)
    //{
    //    Text socialSecurityNumber = 
    //        texts.Find(item => item.transform.name == "Patient_SocialSecurityNumber");

    //    if (socialSecurityNumber == null) socialSecurityNumber = 
    //            transform.Find("Patient_SocialSecurityNumber").GetComponent<Text>();

    //    socialSecurityNumber.text = name;
    //    Debug.Log("socialSecurityNumber = " + name);
    //}

    //public void SetPatientAddress(string address)
    //{
        
    //}
}
