using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Triggers;

public class SlidePuzzleAllClear : MonoBehaviour
{
    public static SlidePuzzleAllClear Instance;
    public Action slideAllClear;
    public List<TextAsset> patterns;
    public int maxCount = 100;
    public int slideCount;
    public bool isWeak;

    [SerializeField]
    private int clearCount;

    [SerializeField]
    GStageLightTrigger lightTrigger = null;

    [SerializeField]
    StoryTrigger completeTrigger = null;

    [SerializeField]
    GameObject StageWall;

    [SerializeField]
    GameObject keyPiece;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        //slideAllClear += completeTrigger.OnTriggered;
        slideAllClear += () => lightTrigger.SetActiveGroup(true);
        isWeak = false;
        slideCount = 0;
        clearCount = 0;
    }

    public void AddSlideCount()
    {
        slideCount++;
        if (slideCount >= maxCount)
        {
            isWeak = true;
        }
    }

    public void AddClearCount()
    {
        clearCount++;
        if (clearCount == 1)
            InvokeClearEvent();
    }

    private void InvokeClearEvent()
    {
        //인벤창에 조각넣기
        keyPiece.SetActive(true);
        Inventory.Instance.PickUpItem(keyPiece.GetComponent<Item>().itemInfo, keyPiece,keyPiece);
        //StageWall.SetActive(false);
        StageWall.GetComponent<WallDissolve>().WallDissolveScene();
        slideAllClear?.Invoke();
    }
}
