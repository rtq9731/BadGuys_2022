using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class PlayerInteractGuide : MonoBehaviour
{
    [SerializeField] float coolTime = 10f;
    [SerializeField] float effectTime = 5f;
    [SerializeField] float scanDist = 30f;
    [SerializeField] Transform skillSphere = null;

    bool isScanning = false;

    Dictionary<Outline, OutlineInfo> originInfoDict = new Dictionary<Outline, OutlineInfo>();

    public void OnInput()
    {
        if(isScanning)
        {
            return;
        }

        Debug.Log("가이드 실행!");
        StartCoroutine(ScanCorutine());
    }

    IEnumerator ScanCorutine()
    {
        isScanning = true;

        skillSphere.localScale = Vector3.zero;
        skillSphere.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", 0.5f);
        skillSphere.gameObject.SetActive(true);

        Collider[] items;
        if ((items = Physics.OverlapSphere(transform.position, scanDist)).Length > 0)
        {
            var result = from item in items
                         where item.transform.GetComponent<IInteractableItem>() != null || item.transform.GetComponent<IInteractAndGetItemObj>() != null
                         select item.transform;

            foreach (var item in result)
            {
                Outline outline = item.GetComponent<Outline>();
                if(outline == null)
                {
                    outline = item.GetComponentInParent<Outline>();
                    if(outline == null)
                    {
                        outline = item.GetComponentInChildren<Outline>();
                    }
                }

                if(originInfoDict.ContainsKey(outline))
                {
                    continue;
                }

                originInfoDict.Add(outline, new OutlineInfo(outline));

                outline.enabled = true;
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 10f;
                outline.OutlineMode = Outline.Mode.OutlineAll;
            }
        }

        skillSphere.DOScale(scanDist, 3f);
        skillSphere.GetComponent<MeshRenderer>().material.DOFloat(0f, "_Alpha", 3f).OnComplete(() => skillSphere.gameObject.SetActive(false));

        yield return new WaitForSeconds(effectTime);

        foreach (var item in originInfoDict)
        {
            item.Key.enabled = false;
            item.Key.OutlineColor = item.Value.outlineColor;
            item.Key.OutlineWidth = item.Value.outlineWidth;
            item.Key.OutlineMode = item.Value.outlineMode;
        }

        yield return new WaitForSeconds(coolTime);
        isScanning = false;
        originInfoDict.Clear();
    }

    class OutlineInfo
    {
        public Color outlineColor = Color.white;
        public Outline.Mode outlineMode = Outline.Mode.OutlineVisible;
        public float outlineWidth = 2f;

        public OutlineInfo(Outline outline)
        {
            outlineColor = outline.OutlineColor;
            outlineMode = outline.OutlineMode;
            outlineWidth = outline.OutlineWidth;
        }
    }

}
