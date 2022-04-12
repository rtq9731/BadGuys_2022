using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePuzzleOutline : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> outLines;
    [SerializeField]
    private GameObject targetOutline;

    private void Awake()
    {
        GetChildren();
        OffAllOutline();
    }

    public void OffAllOutline()
    {
        foreach(GameObject line in outLines)
        {
            line.SetActive(false);
        }
    }

    public void OnOutLine(string name)
    {
        if (targetOutline != null)
            targetOutline.SetActive(false);

        GameObject Obj = FindObj(name);
        if (Obj != null)
        {
            Obj.SetActive(true);
            targetOutline = Obj;
        }
        else
            Debug.LogError("이름이 같은 블럭이 없음");
    }

    private GameObject FindObj(string name)
    {
        foreach (GameObject line in outLines)
        {
            if (line.name == name)
                return line;
        }

        return null;
    }

    private void GetChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            outLines.Add(transform.GetChild(i).gameObject);
        }
    }
}
