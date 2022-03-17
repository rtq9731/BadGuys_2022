using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureInfo : MonoBehaviour
{
    public Image[] picturePieces;
    public int[] pieceColorCode;


    private void Start()
    {
       
    }

    public int[] GetPictureInfo()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            picturePieces[i] = transform.GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            pieceColorCode[i] = picturePieces[i].color.GetHashCode();
        }
        return pieceColorCode;
    }
}
