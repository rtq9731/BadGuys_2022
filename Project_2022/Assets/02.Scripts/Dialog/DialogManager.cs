using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

}

[CreateAssetMenu(fileName = "DialogDatas", menuName = "ScriptableObject/Dialog")]
public class DialogDatas : ScriptableObject
{
    List<DialogData> dialogDatas = new List<DialogData>();

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
    public int id = 0;
    public string str = "";
    public string name = "";
}
