using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureInfo : MonoBehaviour
{
    public Image[] picturePieces;
    public int[] pieceColorCode;

    public int btnCount;

    private void Start()
    {
        
    }

    public int[] GetPictureInfo()
    {
        for (int i = 0; i < btnCount; i++)
        {
            picturePieces[i] = transform.GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < btnCount; i++)
        {
            pieceColorCode[i] = picturePieces[i].color.GetHashCode();
        }
        return pieceColorCode;
    }
}
