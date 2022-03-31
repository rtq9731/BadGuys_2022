using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentsBtn : MonoBehaviour
{
    public GameObject[] childBtns;
    public Text btnText;
    [SerializeField]
    OpinionBtnManager OBManager;
    public bool currect;
    [SerializeField]
    private string answerText;

    private void Awake()
    {
        btnText.text = "";
        currect = false;
        CloseParentsBtn();
        GetComponent<Button>().interactable = false;
    }

    public void CloseParentsBtn()
    {
        for (int i = 0; i < childBtns.Length; i++)
        {
            childBtns[i].SetActive(false);
        }
    }

    public void Push_Btn()
    {
        OBManager.CloseAllParents();

        for (int i = 0; i < childBtns.Length; i++)
        {
            childBtns[i].SetActive(true);
        }
    }

    public void Child_Btn(string text)
    {
        btnText.text = text;

        if (btnText.text == answerText)
            currect = true;
        else
            currect = false;

        CloseParentsBtn();
        OBManager.CheckAllParents();
    }
}
