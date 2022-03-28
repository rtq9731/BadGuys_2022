using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sculpture_Puzzle : MonoBehaviour, IInteractAndGetItemObj, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] List<SculptureKeyAndObj> sculptureKeyAndObjs = new List<SculptureKeyAndObj>();

    [SerializeField] Outline outline = null;
    [SerializeField] float removeDuration = 2f;

    public event System.Action _onPlayerMouseEnter = null;
    public event System.Action _onComplete = null;

    Inventory inventory = null;
    InventoryInput invenInput = null;

    bool isComplete = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        invenInput = FindObjectOfType<InventoryInput>();
        outline.enabled = false;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        SculptureKeyAndObj obj = sculptureKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            if (sculptureKeyAndObjs.Count < 1)
            {
                obj.MakeSculptureObj(removeDuration, _onComplete);
            }

            invenInput.RemoveItem();
            obj.MakeSculptureObj(removeDuration, () => { });
        }
    }

    public void OnPlayerMouseEnter()
    {
        _onPlayerMouseEnter?.Invoke();
        SculptureKeyAndObj obj = sculptureKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            outline.enabled = true;
        }
    }

    public void OnPlayerMouseExit()
    {
        outline.enabled = false;
    }

    [System.Serializable]
    public class SculptureKeyAndObj
    {
        public ItemInfo keyItem = null;
        public GameObject sculptureObj = null;

        public void MakeSculptureObj(float duration, System.Action callBack)
        {
            sculptureObj.GetComponent<MeshRenderer>().material.DOFloat(200, "_NoiseStrength", duration).OnComplete(() =>
            {
                callBack?.Invoke();
            });
        }
    }
}
