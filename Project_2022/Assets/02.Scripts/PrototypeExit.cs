using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PrototypeExit : MonoBehaviour
{
    [SerializeField]
    private Image exitImage;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("클리어 로딩");
        if(other.CompareTag("Player"))
        {
            //로딩
            Debug.Log("데모 클리어");
            exitImage.gameObject.SetActive(true);

            exitImage.transform.DOScale(new Vector3(25, 25, 25), 2f).OnComplete(() =>
            {
                GameManager.Instance.GameClear(1);
                  LoadingManager.LoadScene("Title", true);
            });
        }
    }


}
