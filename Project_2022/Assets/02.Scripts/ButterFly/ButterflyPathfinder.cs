using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyPathfinder : MonoBehaviour
{
    private enum ButterflyState
    {
        NONE,
        IDLE,
        MOVING
    }

    [System.Serializable]
    public class ButterflyPath
    {
        [SerializeField] public List<Transform> paths = new List<Transform>();
    }

    [SerializeField] List<ButterflyPath> butterflyPaths = new List<ButterflyPath>();

    ButterflyState state = ButterflyState.IDLE;
    [SerializeField] Transform butterfly = null;
    [SerializeField] Animator butterflyAnim = null;
    Transform curDestTrm = null;

    int hashButterflyFly = 0;
    int hashButterflyIDLE = 0;

    int curDest = -1;

    [SerializeField] float speed = 1;

    private void Start()
    {
        MoveToNext();

        hashButterflyFly = Animator.StringToHash("bFly");
        hashButterflyIDLE = Animator.StringToHash("bIdle");
    }

    public void UpdateState()
    {
        switch (state)
        {
            case ButterflyState.NONE:
                break;
            case ButterflyState.IDLE:
                break;
            case ButterflyState.MOVING:
                Butterfly_Move();
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
        butterflyAnim.SetBool(hashButterflyFly, true);
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

        yield return null;
    }
}
