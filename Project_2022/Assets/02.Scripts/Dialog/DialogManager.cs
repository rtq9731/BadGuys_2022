using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("상하좌우 빈 공간입니다")]
    public FreeSpace freeSpace = new FreeSpace();
    public int padding = 0;
    public int limitPanelCount = 0;

    public DialogPanel panelPrefab = null;
    List<DialogPanel> dialogs = new List<DialogPanel>();

    RectTransform myRect = null;

    int curOrder = 0;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x + freeSpace.left + freeSpace.right, myRect.sizeDelta.y + freeSpace.down + freeSpace.up);
        StartCoroutine(Test());
    }

    private void CreateDialogPanel(string text, Color color)
    {
        DialogPanel dialogPanel = dialogs.Find(x => !x.gameObject.activeSelf);
        if(dialogPanel == null)
        {
            if(dialogs.Count <= limitPanelCount + 1)
            {
                dialogPanel = Instantiate<DialogPanel>(panelPrefab, transform);
                dialogs.Add(dialogPanel);
            }

            if(dialogs.Count >= limitPanelCount + 1)
            {
                dialogs.Sort((x, y) => -x.order.CompareTo(y.order));
                dialogs[dialogs.Count - 1].SetActiveFalseImmediately();
                dialogPanel = dialogs[0];
            }
        }
        dialogPanel.SetActive(true, color);
    }

    IEnumerator Test()
    {
        CreateDialogPanel("AI : M.A.M의 사용 설명 시스템에 오신 것을 환영합니다.", Color.red);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : 이하는 본 기기의 사용 설명입니다.", Color.green);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : E를 누르시면 특정 아이템을 수집할 수 있습니다.", Color.blue);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : 수집된 아이템은 휠과 숫자키를 통해 선택 할 수 있습니다.", Color.black);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : 이하 생략하겠습니다.", Color.cyan);
        yield return new WaitForSeconds(0.3f);
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
    public Color color = Color.black;
    public string str = "";
    public string name = "";
}
