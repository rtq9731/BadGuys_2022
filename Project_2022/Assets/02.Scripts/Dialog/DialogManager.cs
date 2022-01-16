using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("»óÇÏÁÂ¿ì ºó °ø°£ÀÔ´Ï´Ù")]
    public FreeSpace freeSpace = new FreeSpace();
    public int padding = 0;
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
        float myHeight = 0f;
        for (int i = 0; i < dialogs.FindAll(x => x.gameObject.activeSelf).Count; i++)
        {
            dialogs[i].rectTrm.anchoredPosition = new Vector2(0, freeSpace.down + padding + (i + 1) * dialogs[i].rectTrm.rect.height);
            myHeight += dialogs[i].rectTrm.rect.height;
        }
    }

    private void Update()
    {
        Resize();
    }

    public DialogPanel CreateDialogPanel(string str)
    {
        DialogPanel panel = dialogs.Find(x => !x.gameObject.activeSelf);
        if (panel == null)
        {
            if (dialogs.Count <= limitPanelCount)
            {
                panel = Instantiate<DialogPanel>(panelPrefab);
                dialogs.Add(panel);
            }
            else
            {
                dialogs.Find(x => x.order == 0);
            }
        }

        panel.SetActive(true, () => { }, str);

        return null;
    }

    [System.Serializable]
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
