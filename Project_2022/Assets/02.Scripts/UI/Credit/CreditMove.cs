using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMove : MonoBehaviour
{
    [SerializeField] RectTransform creditRect;
    [SerializeField] float moveSpeed = 5f;
    
    public void SetMoveCredit()
    {
        gameObject.SetActive(true);
        StartCoroutine(CreditCroutine());
    }

    IEnumerator CreditCroutine()
    {
        UIManager.Instance.OnCutScene();

        while (creditRect.anchoredPosition.y <= creditRect.rect.height)
        {
            creditRect.anchoredPosition += Vector2.up * Time.deltaTime * moveSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");

        yield return null;
    }
}
