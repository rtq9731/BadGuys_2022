using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PictureBtnManager : MonoBehaviour
{
    public ColorChangeBtn[] colorChangeBtns;
    public PictureAnswer correctPicture;

    public IEnumerator ClearColorPuzzle()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (var item in colorChangeBtns)
        {
            item.transform.DOScale(0, 0.4f);
            item.transform.DOLocalMoveY(-0.15f, 0.4f).OnComplete(() =>
            {
                item.transform.parent.GetChild(1).DOLocalMoveY(-0.1f, 0.5f);
                item.transform.parent.GetChild(1).DOScale(0, 0.5f);
            });
        }
        yield return new WaitForSeconds(0.5f);

        Debug.Log(colorChangeBtns[0].transform.parent.parent);
        correctPicture.transform.DOLocalMoveZ(-0.4f, 1.2f);
        colorChangeBtns[0].transform.parent.parent.DOLocalMoveZ(-12.45f, 1.2f);
    }
}
    