using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDialog : MonoBehaviour
{
    public List<DialogDatas> normalDialogs;
    public DialogDatas gooutDialogs;
    public List<DialogDatas> comeinDialogs;
    public DialogDatas detectionDialogs;

    private bool[] checkers;

    private void Start()
    {
        checkers = new bool[4] { true, true, true, true };
    }

    public void NormalDialogOn(int count)
    {
        ResetChecker(0);
        if (checkers[0])
        {
            DialogManager.Instance.SetDialogData(normalDialogs[count].GetDialogs());
            checkers[0] = false;
        }
    }

    public void GoOutDialogOn()
    {
        ResetChecker(1);
        if (checkers[1])
        {
            DialogManager.Instance.SetDialogData(gooutDialogs.GetDialogs());
            checkers[1] = false;
        }
    }

    public void ComeInDialogOn(int count)
    {
        ResetChecker(2);
        if (checkers[2])
        {
            DialogManager.Instance.SetDialogData(comeinDialogs[count].GetDialogs());
            checkers[2] = false;
        }
    }

    public void DetectionDialogOn()
    {
        ResetChecker(3);
        if (checkers[3])
        {
            DialogManager.Instance.SetDialogData(detectionDialogs.GetDialogs());
            checkers[3] = false;
        }
    }

    private void ResetChecker(int self)
    {
        bool temp = false;

        if (self < 4 && self >= 0)
        {
            temp = checkers[self];
        }

        checkers[0] = true;
        checkers[1] = true;
        checkers[2] = true;
        checkers[3] = true;

        if (self < 4 && self >= 0)
        {
            checkers[self] = temp;
        }
    }
}
