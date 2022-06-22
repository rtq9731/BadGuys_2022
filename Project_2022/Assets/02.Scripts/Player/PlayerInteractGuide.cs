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

    Dictionary<Transform, OutlineInfo> originInfoDict = new Dictionary<Transform, OutlineInfo>();

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
        skillSphere.GetComponent<MeshRenderer>().material.DOFade(0.1960784f, 0f);

        RaycastHit[] items;
        if ((items = Physics.SphereCastAll(transform.position, scanDist, Vector3.up, 100)).Length > 0)
        {
            foreach (var item in items)
            {
                Debug.Log(item);
            }

            var result = from item in items
                         where item.transform.GetComponent<IInteractableItem>() != null
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

                originInfoDict.Add(item ,new OutlineInfo(outline));

                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 10f;
                outline.OutlineMode = Outline.Mode.OutlineAll;
            }

            foreach (var item in result)
            {
            }
        }

        skillSphere.DOScale(scanDist, 3f);
        skillSphere.GetComponent<MeshRenderer>().material.DOFade(0, 3f);

        yield return new WaitForSeconds(effectTime);

        foreach (var item in originInfoDict)
        {
            Outline outline = item.Key.GetComponent<Outline>();
            outline.OutlineColor = item.Value.outlineColor;
            outline.OutlineWidth = item.Value.outlineWidth;
            outline.OutlineMode = item.Value.outlineMode;
        }

        yield return new WaitForSeconds(coolTime);
        isScanning = false;
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
