using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("�����¿� �� �����Դϴ�")]
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
        CreateDialogPanel("AI : M.A.M�� ��� ���� �ý��ۿ� ���� ���� ȯ���մϴ�.", Color.red);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : ���ϴ� �� ����� ��� �����Դϴ�.", Color.green);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : E�� �����ø� Ư�� �������� ������ �� �ֽ��ϴ�.", Color.blue);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : ������ �������� �ٰ� ����Ű�� ���� ���� �� �� �ֽ��ϴ�.", Color.black);
        yield return new WaitForSeconds(0.3f);
        CreateDialogPanel("AI : ���� �����ϰڽ��ϴ�.", Color.cyan);
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
