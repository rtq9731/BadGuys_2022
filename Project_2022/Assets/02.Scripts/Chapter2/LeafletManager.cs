using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafletManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> leaflets;
    [SerializeField]
    private GameObject leafletsParent;
    [SerializeField]
    private EmphasizeCircle circle;

    public bool isChase;

    private void Awake()
    {
        if (FindObjectOfType<EmphasizeCircle>() != null && circle == null)
            circle = FindObjectOfType<EmphasizeCircle>();

        if (leafletsParent == null && GameObject.Find("Leaflets") != null)
            leafletsParent = GameObject.Find("Leaflets");

        Getleafelts();
        isChase = false;
    }

    private void Start()
    {
        StartCoroutine(CheckAndSetCircle());
    }

    private void Getleafelts()
    {
        for (int i = 0; i < leafletsParent.transform.childCount; i++)
        {
           leaflets.Add(leafletsParent.transform.GetChild(i).gameObject);
        }
    }

    private bool CheckActive()
    {
        foreach (GameObject item in leaflets)
        {
            if (item.activeSelf)
                return true;
        }

        return false;
    }

    public void StartChase()
    {
        isChase = true;
    }

    private IEnumerator CheckAndSetCircle()
    {
        int num = 0;

        while (CheckActive())
        {
            if (UIManager.Instance.isOnCutScene)
            {
                circle.EmphasizeOff();
                Debug.Log("¿ø ²¨Áü");
                break;
            }

            if (leaflets[num].activeSelf == false)
            {
                num++;

                if (num >= leaflets.Count)
                    break;
                else
                    continue;
            }

            if (!circle.isEmphasize)
            {
                circle.EmphasizeOn(leaflets[num]);
            }
            
            yield return null;
        }
    }
}
