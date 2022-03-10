using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class StateMentOpinionManager : MonoBehaviour
{
    public static StateMentOpinionManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("StateMentOpinionManager가 2개 이상입니다.");
            Destroy(this);
        }

        startCameraMove = false;
    }

    [SerializeField]
    private List<GameObject> movePos;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private float movingTime = 3f;
    //[SerializeField]
    //private float typingSpeed = 2f;

    public int stepNum;
    public int opinionStep = 1;
    public UnityEvent<int> Cameramoving;
    public List<Text> texts;

    private bool startCameraMove;

    private void Start()
    {
        MoveStep(0);
        stepNum = 0;
        StartCoroutine(TextClear());
        StartCoroutine(StartCutScene());
    }

    private void Update()
    {
        if (startCameraMove == true)
        {
            if (Input.GetKeyDown(KeyCode.W) && stepNum > 0)
            {
                stepNum--;
                if (stepNum < 0) stepNum = 0;
                MoveStep(stepNum);
                Cameramoving.Invoke(stepNum);
            }

            if (Input.GetKeyDown(KeyCode.S) && stepNum < movePos.Count - 1)
            {
                stepNum++;
                if (stepNum > movePos.Count - 1) stepNum = movePos.Count - 1;
                MoveStep(stepNum);
                Cameramoving.Invoke(stepNum);
            }
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

    public void FindTextAndSetbyNum(int textNum, string textContent)
    {
        Text target = texts[textNum];

        if (target == null)
        {
            target = transform.Find(texts[textNum].name).GetComponent<Text>();
        }

        StartCoroutine(TextTyping(target, textContent));
        Debug.Log(textNum + " = " + textContent);
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

    private IEnumerator StartCutScene()
    {
        float downSec = 8f;

        yield return new WaitForSeconds(5f);
        paper.transform.DOMove(movePos[opinionStep].transform.position, downSec);
        yield return new WaitForSeconds(downSec);
        startCameraMove = true;
        stepNum = opinionStep;
        Cameramoving.Invoke(stepNum);
    }

    private IEnumerator TextTyping(Text targetText, string content)
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i <= content.Length; i++)
        {
            targetText.text = content.Substring(0, i);

            float delay = Random.Range(0.1f, 0.2f);
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
