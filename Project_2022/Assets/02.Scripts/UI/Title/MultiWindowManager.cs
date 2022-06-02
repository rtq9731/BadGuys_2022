using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiWindowManager : MonoBehaviour
{
    [SerializeField] List<MultiWindowCell> windowCells = new List<MultiWindowCell>();
    private void Awake()
    {
        windowCells.ForEach(cell =>
        {
            cell.OnObjEnable += () => SortWindows(cell);
        });
    }

    public void SortWindows(MultiWindowCell obj)
    {
        obj.transform.SetAsLastSibling();
    }
}
