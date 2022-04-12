using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogDataList", menuName = "ScriptableObject/Dialog")]
public class DialogDatas : ScriptableObject
{
    [SerializeField] List<DialogData> dialogDatas = new List<DialogData>();

    public List<DialogData> GetDialogs(int firstID, int lastID)
    {
        return (from data in dialogDatas
                where data.id >= firstID
                where data.id <= lastID
                orderby data.id
                select data).ToList();
    }

    public List<DialogData> GetDialogs()
    {
        dialogDatas.Sort((x, y) => -x.id.CompareTo(y.id));
        return dialogDatas;
    }
}

[System.Serializable]
public class DialogData
{
    public Action callBack;
    public int id = 0;
    public Color color = Color.white;
    public string str = "";
    public string name = "";

    public DialogData(string name, string str, Color color, int id = 0)
    {
        this.name = name;
        this.str = str;
        this.color = color;
        this.id = id;
    }
}