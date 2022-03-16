using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureAnswer : MonoBehaviour
{
    public Image[] picturePieces;

    public int[] pieceColorCode;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            picturePieces[i] = transform.GetChild(i).GetComponent<Image>();
        }

        FindCorrectColorCode();
    }

    void FindCorrectColorCode()
    {
        for(int i = 0; i < transform.childCount; i ++)
        {
            pieceColorCode[i] = picturePieces[i].color.GetHashCode();
        }
    }
}
