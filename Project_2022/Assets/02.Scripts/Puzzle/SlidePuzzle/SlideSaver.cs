using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SlideSaver : MonoBehaviour
{
    public SlidePuzzleManager manager;

    public void SavePuzzlePos()
    {
        string filePath = "Assets/SlidePuzzleSave/Save";
        StreamWriter sw;

        int count = 0;
        while (true)
        {
            count++;
            if (File.Exists(filePath + count + ".txt") == false)
            {
                sw = new StreamWriter(filePath + count + ".txt");
                break;
            }
        }

        List<Vector3> pieces = manager.GetNowPoses();

        Debug.Log(pieces.Count);
        for (int i = 0; i < pieces.Count; i++)
        {
            sw.Write(pieces[i].x + ",");
            sw.Write(pieces[i].y + ",");
            sw.Write(pieces[i].z + "\n");
        }

        sw.Flush();
        sw.Close();

        Debug.Log("저장 완료");
    }
}
