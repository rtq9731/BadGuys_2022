using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StateMentOpinionTextReader : MonoBehaviour
{

    StateMentOpinionManager SMOM;
    private string[] textFilesName;
    private Text[] loadTexts;
    private string filePath = Application.streamingAssetsPath;

    private void Awake()
    {
        SMOM = GetComponent<StateMentOpinionManager>();
        textFilesName = Directory.GetFiles(filePath, "*.txt");
    }

    void Start()
    {
        //StartCoroutine(ExampleMaker());
        TextLoadbyNum(0);
    }

    public void TextLoadbyNum(int num)
    {
        //if (num == 0)
        //{
        //    Debug.LogError("Text 0번은 example 입니다.");
        //    return;
        //}
        if (num > textFilesName.Length)
        {
            Debug.LogError("아직 만들어지지 않았거나, 현재 있는 소견서의 수를 넘는 숫자입니다.");
            return;
        }

        FileInfo fileInfo = new FileInfo(textFilesName[num]);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(textFilesName[num]);
            value = reader.ReadToEnd();
            reader.Close();

            StartCoroutine(TextSubStrig(value));
        }
        else
            Debug.LogError("파일이 없습니다.");
    }

    IEnumerator TextSubStrig(string Text)
    {
        string[] opinionText = Text.Split('#');

        for (int i = 0; i < SMOM.texts.Count; i++)
        {
            SMOM.FindTextAndSetbyNum(i, opinionText[i]);
            yield return new WaitForSeconds(0.3f);
        }

        yield return null;
    }


    // 파일 만드는 함수 ( 한번 이제 안씀 )

    //IEnumerator ExampleMaker() 
    //{
    //    string exampleFilePath = Path.Combine(filePath, "00ExampleStateMentOpinion.txt");

    //    DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));

    //    if (!directoryInfo.Exists)
    //    {
    //        directoryInfo.Create();
    //    }

    //    FileStream fileStream
    //        = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

    //    StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

    //    for (int i = 0; i < SMOM.texts.Count; i++)
    //    {
    //        writer.WriteLine(SMOM.texts[i].name + "#");
    //    }

    //    writer.Close();
    //    yield return new WaitForSeconds(0f);
    //}
}
