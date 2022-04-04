using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingImage : MonoBehaviour
{
    public Image[] loadingImages;

    int index = 0;

    Sequence scaleSequence;
    Sequence colorSequence;
    void Start()
    {
        scaleSequence = DOTween.Sequence();
        colorSequence = DOTween.Sequence();

        scaleSequence.SetAutoKill(false); 
        colorSequence.SetAutoKill(false);

        colorSequence.Append(loadingImages[0].DOColor(Color.white, 0.2f))
        .AppendInterval(0.1f)
        .AppendCallback(() => { loadingImages[0].DOColor(Color.gray, 0.2f); })
        .Insert(0.1f, loadingImages[1].DOColor(Color.white, 0.2f))
        .AppendInterval(0.1f)
        .AppendCallback(() => { loadingImages[1].DOColor(Color.gray, 0.2f); })
        .Insert(0.2f, loadingImages[2].DOColor(Color.white, 0.2f))
        .AppendInterval(0.1f)
        .AppendCallback(() => { loadingImages[2].DOColor(Color.gray, 0.2f); })
        .AppendInterval(0.5f)
        .SetLoops(-1);

        scaleSequence.Append(loadingImages[0].transform.DOScale(1.2f, 0.2f))
        .AppendInterval(0.1f)
        .AppendCallback(() => { loadingImages[0].transform.DOScale(0.5f, 0.2f); })
        .Insert(0.1f, loadingImages[1].transform.DOScale(1.2f, 0.2f))
        .AppendInterval(0.1f)
        .AppendCallback(() => { loadingImages[1].transform.DOScale(0.5f, 0.2f); })
        .Insert(0.2f, loadingImages[2].transform.DOScale(1.2f, 0.2f))
        .AppendInterval(0.1f)
        .AppendCallback(() => { loadingImages[2].transform.DOScale(0.5f, 0.2f); })
        .AppendInterval(0.5f)
        .SetLoops(-1);
    }
}
