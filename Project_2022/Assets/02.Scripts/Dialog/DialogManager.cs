using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("상하좌우 빈 공간입니다")]
    public FreeSpace freeSpace = new FreeSpace();
    public int limitPanelCount = 0;

    public DialogPanel panelPrefab = null;
    List<DialogPanel> dialogs = new List<DialogPanel>();

    RectTransform myRect = null;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    public void Refresh()
    {

    }

    public void Resize()
    {

    }

    public DialogPanel CreateDialogPanel()
    {

        if(dialogs.Count >= 1 && dialogs.Find(x => !x.gameObject.activeSelf))
        {

        }
        else
        {
            
        }

        return null;
    }

    public class FreeSpace
    {
        public int left = 0;
        public int right = 0;
        public int up = 0;
        public int down = 0;
    }
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
