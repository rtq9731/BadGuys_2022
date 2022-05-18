using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractHelpPanelCaller : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float coolTime = 5f;

    [SerializeField] [TextArea] string msg = "";

    float timer = 0f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.activeSelf)
        {
            Debug.Log(gameObject);
            Destroy(this);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= coolTime)
        {
            UIInteractHelpPanelManager.Instance.ShowHelpPanel(msg);
            Destroy(this);
        }
    }

}
