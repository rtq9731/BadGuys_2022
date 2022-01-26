using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EamilDatas", fileName = "EmailDataSOList")]
public class EmailDataListSO : ScriptableObject
{
    public List<EmailTextData> emailList = new List<EmailTextData>();
}
