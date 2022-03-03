using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiWindowManager : MonoBehaviour
{
    [SerializeField] List<MultiWindowCell> windowCells = new List<MultiWindowCell>();
    private void Awake()
    {
        windowCells.ForEach(item => item.OnObjEnable += SortWindows);
        windowCells.ForEach(item => item.OnObjDisable += SortWindows);
    }

    public void SortWindows()
    {
        windowCells.Sort();
    }
}
