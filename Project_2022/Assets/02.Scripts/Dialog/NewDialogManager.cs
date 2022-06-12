using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogManager : MonoBehaviour
{
    List<NewDialogPanel> dialogPanelPool = new List<NewDialogPanel>();
    Stack<Stack<char>> dialogDatas = new Stack<Stack<char>>();

    Stack<char> curDialog = new Stack<char>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }


    public void SetDialog(List<DialogData> datas)
    {

        datas.Sort((x, y) => -x.id.CompareTo(y.id));
        foreach (var item in datas)
        {

        }

        datas.Sort((x, y) => x.id.CompareTo(y.id));
        foreach (var item in datas)
        {
            UIManager.Instance.SetStopMenuDialog($"{item.name} : {item.str}", item.color);
        }

        if (cor == null)
        {
            cor = StartCoroutine(PlayDialog());
        }
    }

    private Stack<char> DialogDataToStack(DialogData data)
    {
        Stack<char> result = new Stack<char>();

        for (int i = data.str.Length; i > 0; i--)
        {
            result.Push(data.str[i]);
        }

        result.Push(' ');
        result.Push(':');
        result.Push(' ');

        for (int i = data.name.Length; i > 0; i--)
        {
            result.Push(data.name[i]);
        }

        return result;
    }

    public void ClearALLDialog()
    {
        while (dialogDatas.Count > 0)
        {
            dialogDatas.Pop().Clear();
        }
        dialogDatas.Clear();

        dialogPanelPool.ForEach(x => x.RemoveNow());
    }

    IEnumerator PlayDialog()
    {
        float timer = 0f;

        while (dialogDatas.Count > 0)
        {

            if(curDialog == null)
            {
                curDialog = dialogDatas.Pop();
            }

            CreateDialogPanel(data.name, data.str, data.color);

            timer = 0f;
            while (timer < data.str.Length * 0.1f + 0.5f) // 글자 수 + 시간만큼 기다린다
            {
                if (!GameManager.Instance.IsPause)
                    timer += Time.deltaTime;

                yield return null;
            }
        }
        yield return null;
    }
}
