using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEmphasis : MonoBehaviour
{
    [SerializeField] public EmphasizeCircle circle;

    [SerializeField] List<GameObject> emphasisObjs = new List<GameObject>();
    [SerializeField] List<Vector3> objsPosition = new List<Vector3>();
    
    public int idx = 0;
    bool isActive = true;
    void Start()
    {
        foreach(var item in emphasisObjs)
        {
            objsPosition.Add(item.transform.position);
        }

        StartCoroutine(ChangeObj());
    }

    public IEnumerator ChangeObj()
    {
        float posX = 0;
        float movePosX = 0;
        while(isActive)
        {
            if (UIManager.Instance.isOnCutScene)
            {
                //circle.EmphasizeOff();
                //Debug.Log("¿ø ²¨Áü");
                //break;
            }

            posX = emphasisObjs[idx].transform.position.x;
            movePosX = objsPosition[idx].x;

            if (Mathf.Round(posX) != Mathf.Round(movePosX))
            {
                idx++;

                circle.isChangeObj = true;

                if (idx >= emphasisObjs.Count)
                    break;
            }

            if (!circle.isEmphasize)
            {
                circle.EmphasizeOn(emphasisObjs[idx]);
            }

            yield return null;
        }
    }
}
