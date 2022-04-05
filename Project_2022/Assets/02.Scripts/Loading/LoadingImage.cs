using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingImage : MonoBehaviour
{
    public Image[] loadingImages;

    int index = 0;

    Coroutine loadingCor = null;

    Sequence scaleSequence;
    Sequence colorSequence;
    void Start()
    {
        loadingCor = StartCoroutine(LoadingCorutine());

        //scaleSequence = DOTween.Sequence();
        //colorSequence = DOTween.Sequence();

        //scaleSequence.SetAutoKill(false); 
        //colorSequence.SetAutoKill(false);

        //for (int i = 0; i < loadingImages.Length; i++)
        //{
        //    int y = i;
        //    if (i == 0)
        //    {
        //        colorSequence.Append(loadingImages[0].DOColor(Color.white, 0.2f).OnComplete(() => Debug.Log("ぞし" + y)))
        //        .AppendInterval(0.1f)
        //        .AppendCallback(() => { loadingImages[0].DOColor(Color.gray, 0.2f); });

        //        scaleSequence.Append(loadingImages[0].transform.DOScale(1.2f, 0.2f).OnComplete(() => Debug.Log("ぞし" + y)))
        //        .AppendInterval(0.1f)
        //        .AppendCallback(() => { loadingImages[0].transform.DOScale(0.5f, 0.2f); });

        //        continue;
        //    }

        //    colorSequence.Insert(0.1f, loadingImages[y].DOColor(Color.white, 0.2f).OnComplete(() => Debug.Log("ぞし" + y)))
        //    .AppendInterval(0.1f)
        //    .AppendCallback(() => { loadingImages[y].DOColor(Color.gray, 0.2f); });

        //    scaleSequence.Insert(0.1f, loadingImages[y].transform.DOScale(1.2f, 0.2f).OnComplete(() => Debug.Log("ぞし" + y)))
        //    .AppendInterval(0.1f)
        //    .AppendCallback(() => { loadingImages[y].transform.DOScale(0.5f, 0.2f); });
        //}

        //scaleSequence.AppendInterval(0.5f).SetLoops(-1);
        //colorSequence.AppendInterval(0.5f).SetLoops(-1);
    }

    private IEnumerator LoadingCorutine()
    {
        int curCircle = 3;
        while (true)
        {
            loadingImages[curCircle % loadingImages.Length].color = Color.white;
            loadingImages[curCircle % loadingImages.Length].transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            loadingImages[GetBackCircleIDX(-1, curCircle) % loadingImages.Length].color = new Color(0.9f, 0.9f, 0.9f, 1f);
            loadingImages[GetBackCircleIDX(-1, curCircle) % loadingImages.Length].transform.localScale = new Vector3(1f, 1f, 1f);
            loadingImages[GetBackCircleIDX(-2, curCircle) % loadingImages.Length].color = new Color(0.8f, 0.8f, 0.8f, 1f);
            loadingImages[GetBackCircleIDX(-2, curCircle) % loadingImages.Length].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            loadingImages[GetBackCircleIDX(-3, curCircle) % loadingImages.Length].color = new Color(0.7f, 0.7f, 0.7f, 1f);
            loadingImages[GetBackCircleIDX(-3, curCircle) % loadingImages.Length].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.075f);
            curCircle++;
        }
    }

    private int GetBackCircleIDX(int minus, int curIdx)
    {
        if(curIdx + minus < 0)
        {
            return (loadingImages.Length) + minus;
        }
        else
        {
            return curIdx + minus;
        }
    }

    private void OnDestroy()
    {
        if(loadingCor != null)
        {
            StopCoroutine(loadingCor);
        }
    }
}
