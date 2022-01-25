using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailInfoPanel : MonoBehaviour
{
    [SerializeField] Text textTitle;
    [SerializeField] Text textSender;
    [SerializeField] Text textSenderPart;
    [SerializeField] Text textDate;
    [SerializeField] Text text;

    void InitEmailPanel(EmailData data)
    {
        textDate.text = data.sendTime.ToString("yyyy-MM-dd");
        EmailTextData textData = GameManager._instance.GetEmailText(data.textDataID);
        textTitle.text = textData.title;
        textSender.text = textData.sender;
        textSenderPart.text = textData.partName;
        text.text = textData.text;
    }
}
