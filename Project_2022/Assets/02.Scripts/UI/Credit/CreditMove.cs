using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMove : MonoBehaviour
{
    [SerializeField] RectTransform creditRect;
    [SerializeField] float moveSpeed = 5f;

    bool isSetTimer = false;

    float timer = 0f;

    private void Start()
    {
        SetMoveCredit();
    }

    public void SetMoveCredit()
    {
        StartCoroutine(CreditCroutine());
    }

    IEnumerator CreditCroutine()
    {
        while (creditRect.anchoredPosition.y <= creditRect.rect.height)
        {
            creditRect.anchoredPosition += Vector2.up * Time.deltaTime * moveSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        yield return null;
    }
}
