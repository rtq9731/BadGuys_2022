using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogDataList", menuName = "ScriptableObject/Dialog")]
public class DialogDatas : ScriptableObject
{
    public List<DialogData> dialogDatas = new List<DialogData>();

    public List<DialogData> GetDialogs(int firstID, int lastID)
    {
        return (from data in dialogDatas
                where data.id >= firstID
                where data.id <= lastID
                select data).ToList();
    }
}

[System.Serializable]
public class DialogData
{
    public Action callBack;
    public int id = 0;
    public Color color = Color.black;
    public string str = "";
    public string name = "";
}