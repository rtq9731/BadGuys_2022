using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class StateMentOpinionManager : MonoBehaviour
{
    public static StateMentOpinionManager Instance;
    [SerializeField]
    private List<GameObject> movePos;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private float movingTime = 3f;

    public int opinionStep = 1;
    public List<Text> texts;
    public bool canCameraMove = true;
    public bool isChoose = false;
    public float moveAmount = 10f;

    private bool startCameraMove;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("StateMentOpinionManager가 2개 이상입니다.");
            Destroy(this);
        }

        startCameraMove = false;
        isChoose = false;
        paper.transform.DOMove(movePos[0].transform.position, movingTime);
        StartCoroutine(TextClear());
    }

    private void Update()
    {
        if (startCameraMove && canCameraMove)
        {
            Vector2 wheelInput2 = Input.mouseScrollDelta;
            if (wheelInput2.y > 0)
            {
                // 휠을 밀어 돌렸을 때의 처리 ↑
                MoveYPos(-moveAmount);
                wheelInput2.y = 0;
            }
            else if (wheelInput2.y < 0)
            {
                // 휠을 당겨 올렸을 때의 처리 ↓
                MoveYPos(moveAmount);
                wheelInput2.y = 0;
            }
        }
    }

    public void StartCameraMove(float wait = 5, float down = 8)
    {
        StartCoroutine(StartCutScene(wait, down));
    }

    public void FindTextAndSet(string textName, string textContent)
    {
        Text target = texts.Find(item => item.transform.name == textName);

        if (target == null)
        {
            target = transform.Find(textName).GetComponent<Text>();
        }

        StartCoroutine(TextTyping(target, textContent));
        Debug.Log(textName + " = " + textContent);
    }

    public void FindTextAndSetbyNum(int textNum, string textContent)
    {
        Text target = texts[textNum];

        if (target == null)
        {
            target = transform.Find(texts[textNum].name).GetComponent<Text>();
        }

        StartCoroutine(TextTyping(target, textContent));
        Debug.Log(textNum + " = " + textContent);
    }

    public void MoveYPos(float amount)
    {
        Debug.Log("asdf");
        Vector3 paperPos = paper.transform.position;

        //if (paperPos.y + amount <= movePos[0].transform.position.y) // 최대로 올라갔는데 더 올라갈 경우
        //    return;
        //amount = movePos[0].transform.position.y - paperPos.y;
        //if (paperPos.y + amount >= movePos[movePos.Count - 1].transform.position.y) // 최대로 내려갔는데 더 내랴갈 경우
        //    return;
        //amount = movePos[movePos.Count - 1].transform.position.z - paperPos.y;

        Vector3 toPos = new Vector3(paperPos.x, paperPos.y + amount, paperPos.z);
        float distance = Mathf.Abs(Mathf.Abs(paperPos.y) - Mathf.Abs(amount));
        paper.transform.DOMove(toPos, distance / movingTime);
    }

    public IEnumerator TextClear()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].text = "";
        }

        yield return new WaitForSeconds(0f);
    }

    private IEnumerator StartCutScene(float waitSec = 5f, float downSec = 8f)
    {
        yield return new WaitForSeconds(waitSec);
        paper.transform.DOMove(movePos[opinionStep].transform.position, downSec);
        yield return new WaitForSeconds(downSec);
        startCameraMove = true;
    }

    private IEnumerator TextTyping(Text targetText, string content)
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i <= content.Length; i++)
        {
            targetText.text = content.Substring(0, i);

            float delay = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(delay);
        }

    }
}
