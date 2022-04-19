using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Emerald : MonoBehaviour , IInteractAndGetItemObj, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] 
    List<PieceKeyObj> pieceKeyObjs = new List<PieceKeyObj>();

    [SerializeField] Outline outline = null;
    [SerializeField] float removeDuration = 2f;

    [SerializeField] GameObject caseButton;
    public event System.Action _onPlayerMouseEnter = null;
    public event System.Action _onComplete = null;

    Inventory inventory = null;
    InventoryInput invenInput = null;

    bool isCorrect = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        invenInput = FindObjectOfType<InventoryInput>();
        outline.enabled = false;
    }
    

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        Debug.Log("조각 넣기");
        PieceKeyObj obj = pieceKeyObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            invenInput.RemoveItem();
            obj.MakePieceKeyObj(removeDuration, () => { });

            if (pieceKeyObjs.FindAll(item => !item.isComplete).Count < 1)
            {
                obj.MakePieceKeyObj(removeDuration, _onComplete);
                outline.enabled = false;
                caseButton.SetActive(true);
                isCorrect = true;
            }
        }
    }

    public void OnPlayerMouseEnter()
    {
        if (!enabled)
            return;

        _onPlayerMouseEnter?.Invoke();
        PieceKeyObj obj = pieceKeyObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void OnPlayerMouseExit()
    {
        if (!enabled)
            return;

        outline.enabled = false;
    }

    public bool CanInteract(ItemInfo itemInfo)
    {
        if (!gameObject.activeSelf || !enabled)
            return false;

        PieceKeyObj obj = pieceKeyObjs.Find(item => item.keyItem == inventory.MainItem);
        return obj != null;
    }

    [System.Serializable]
    public class PieceKeyObj
    {
        public bool isComplete = false;
        public ItemInfo keyItem = null;
        public GameObject pieceKeyObj = null;

        public void MakePieceKeyObj(float duration, System.Action callBack)
        {
            isComplete = true;

            pieceKeyObj.GetComponent<MeshRenderer>().material.DOFloat(200, "_NoiseStrength", duration).OnComplete(() =>
            {
                callBack?.Invoke();
            });
        }
    }
}
