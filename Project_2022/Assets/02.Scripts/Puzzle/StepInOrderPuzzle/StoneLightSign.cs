using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLightSign : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private OrderPuzzle orderPuzzle;
    [SerializeField]
    private GameObject[] rightStoneOrder;

    private bool isProgress;
    void Start()
    {
        rightStoneOrder = orderPuzzle.rightOrder;
    }

    public void Interact(GameObject taker)
    {
        if(isProgress == false)
            StartCoroutine(LightInOrder());
    }

    IEnumerator LightInOrder()
    {
        isProgress = true;
        for (int i = 0; i < rightStoneOrder.Length; i++)
        {
            rightStoneOrder[i].transform.GetChild(0).gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            rightStoneOrder[i].transform.GetChild(0).gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }
        isProgress = false;
    }
}
