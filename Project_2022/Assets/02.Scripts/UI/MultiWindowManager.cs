using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiWindowManager : MonoBehaviour
{
    List<MuitiWindowCell> windowCells = new List<MuitiWindowCell>();

    private void Start()
    {
        windowCells = transform.GetComponentsInChildren<MuitiWindowCell>().ToList();

        windowCells.Sort();
    }
}
