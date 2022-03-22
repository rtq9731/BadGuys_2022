using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButterflyPathfinder : MonoBehaviour
{
    private enum ButterflyState
    {
        NONE,
        IDLE,
        MOVING,
        DISAPPEAR,
        APPEAR
    }

    [System.Serializable]
    public class ButterflyPath
    {
        [SerializeField] public List<Transform> paths = new List<Transform>();
    }

    [SerializeField] List<ButterflyPath> butterflyPaths = new List<ButterflyPath>();

    ButterflyState state = ButterflyState.IDLE;

    [SerializeField] Transform butterfly = null;
    [SerializeField] Transform butterflyDissolve = null;
    [SerializeField] Texture2D butterflyDissolveTex = null;
    List<Material> butterflyDissolveMats = new List<Material>();

    [SerializeField] Animator butterflyAnim = null;
    Transform curDestTrm = null;

    int hashButterflyFLY = 0;
    int hashButterflyIDLE = 0;

    int curDest = -1;

    [SerializeField] float speed = 1;

    private void Start()
    {
        butterflyDissolve.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(item => 
        {
            item.material.SetTexture("_MainTex", butterflyDissolveTex);
            butterflyDissolveMats.Add(item.material);
        });

        hashButterflyFLY = Animator.StringToHash("bFly");
        hashButterflyIDLE = Animator.StringToHash("bIdle");

        ChangeState(ButterflyState.APPEAR);
    }

    public void UpdateState()
    {
        switch (state)
        {
            case ButterflyState.NONE:
                break;
            case ButterflyState.IDLE:
                butterflyAnim.SetBool(hashButterflyFLY, false);
                butterflyAnim.SetBool(hashButterflyIDLE, true);
                break;
            case ButterflyState.MOVING:
                Butterfly_Move();
                break;
            case ButterflyState.DISAPPEAR:
                Butterfly_Disappear();
                break;
            case ButterflyState.APPEAR:
                Butterfly_Appear();
                break;
            default:
                break;
        }
    }

    public void MoveToNext()
    {
        curDest++;
        ChangeState(ButterflyState.MOVING);
    }

    private void ChangeState(ButterflyState newState)
    {
        state = newState;
        UpdateState();
    }

    private void Butterfly_Move()
    {
        StartCoroutine(MovePosition(butterflyPaths[curDest].paths));
        butterflyAnim.SetBool(hashButterflyFLY, true);
    }

    private void Butterfly_Appear()
    {
        Sequence seq = DOTween.Sequence();
        
        butterflyAnim.SetBool(hashButterflyIDLE, true);
        butterflyAnim.SetBool(hashButterflyFLY, false);

        butterflyDissolve.gameObject.SetActive(true);
        butterflyDissolveMats.ForEach(item => seq.Join(item.DOFloat(300, "_NoiseStrength", 5f)));
        seq.OnComplete(() =>
        {
            MoveToNext();
            butterflyDissolve.gameObject.SetActive(false);
            butterfly.gameObject.SetActive(true);
        });
    }

    private void Butterfly_Disappear()
    {
        Sequence seq = DOTween.Sequence();
        butterflyDissolve.gameObject.SetActive(true);
        butterfly.gameObject.SetActive(false);
        butterflyDissolveMats.ForEach(item => seq.Join(item.DOFloat(0, "_NoiseStrength", 5f)));
        seq.OnComplete(() =>
        {
            ChangeState(ButterflyState.IDLE);
        });
    }

    private IEnumerator MovePosition(List<Transform> paths)
    {
        Queue<Transform> pathQueue = new Queue<Transform>();
        paths.ForEach(item => pathQueue.Enqueue(item));
        bool isComplete = false;

        while(pathQueue.Count > 0 || !isComplete)
        {
            if (curDestTrm == null)
            {
                curDestTrm = pathQueue.Dequeue();
                isComplete = false;
            }

            butterfly.LookAt(curDestTrm);
            transform.Translate((curDestTrm.position - transform.position).normalized * speed * Time.deltaTime);
            if(Vector3.Distance(curDestTrm.position, transform.position) <= 0.01f)
            {
                butterfly.rotation = curDestTrm.rotation;
                
                isComplete = true;
                curDestTrm = null;
            }
            
            yield return null;
        }

        ChangeState(ButterflyState.DISAPPEAR);
        yield return null;
    }
}
