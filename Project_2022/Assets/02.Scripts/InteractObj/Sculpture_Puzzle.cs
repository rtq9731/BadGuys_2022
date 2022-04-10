using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Triggers;

public class Sculpture_Puzzle : MonoBehaviour, IInteractAndGetItemObj, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] List<SculptureKeyAndObj> sculptureKeyAndObjs = new List<SculptureKeyAndObj>();

    [SerializeField] Outline outline = null;
    [SerializeField] float removeDuration = 2f;

    [SerializeField] GameObject clearTrigger = null;
    [SerializeField] GameObject wallObj = null;

    [SerializeField] StoryTrigger storyTrigger = null;

    public event System.Action _onPlayerMouseEnter = null;
    public event System.Action _onComplete = null;

    Inventory inventory = null;
    InventoryInput invenInput = null;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        invenInput = FindObjectOfType<InventoryInput>();
        outline.enabled = false;

        _onComplete += storyTrigger.OnTriggered;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        SculptureKeyAndObj obj = sculptureKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            invenInput.RemoveItem();
            obj.MakeSculptureObj(removeDuration, () => { });

            if (sculptureKeyAndObjs.FindAll(item => !item.isComplete).Count < 1)
            {
                obj.MakeSculptureObj(removeDuration, _onComplete);
                wallObj.SetActive(false);
                clearTrigger.SetActive(true);
                enabled = false;
                outline.enabled = false;
            }
        }
    }

    public void OnPlayerMouseEnter()
    {
        if (!enabled)
            return;

        _onPlayerMouseEnter?.Invoke();
        SculptureKeyAndObj obj = sculptureKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            outline.enabled = true;
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

        SculptureKeyAndObj obj = sculptureKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        return obj != null;
    }

    [System.Serializable]
    public class SculptureKeyAndObj
    {
        public bool isComplete = false;
        public ItemInfo keyItem = null;
        public GameObject sculptureObj = null;

        public void MakeSculptureObj(float duration, System.Action callBack)
        {
            isComplete = true;

            sculptureObj.GetComponent<MeshRenderer>().material.DOFloat(200, "_NoiseStrength", duration).OnComplete(() =>
            {
                callBack?.Invoke();
            });
        }
    }
}
