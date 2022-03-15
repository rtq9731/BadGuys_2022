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
        Debug.Log("Ŭ���� �ε�");
        if(other.CompareTag("Player"))
        {
            //�ε�
            Debug.Log("���� Ŭ����");
            exitImage.gameObject.SetActive(true);

            exitImage.transform.DOScale(new Vector3(25, 25, 25), 2f).OnComplete(() =>
            {
                GameManager.Instance.GameClear(1);
                  LoadingManager.LoadScene("Title", true);
            });
        }
    }


}
