using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialExit : Triggers.EmailTrigger
{
    [SerializeField]
    private Image exitImage;

    public System.Action onClear = null;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //�ε�
            Debug.Log("Ʃ�丮�� Ŭ����");

            onClear?.Invoke();

            exitImage.gameObject.SetActive(true);

            exitImage.transform.DOScale(new Vector3(25, 25, 25), 2f).OnComplete(() =>
            {
                GameManager.Instance.GameClear(1);
                  LoadingManager.LoadScene("Title", true);
            });
        }
    }


}
