using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButterFlyScript : MonoBehaviour
{
    public Transform butterfly = null;
    [SerializeField] Texture2D butterflyDissolveTex = null;
    [SerializeField] ParticleSystem ps = null;

    List<Material> butterflyDissolveMats = new List<Material>();

    [SerializeField] Animator butterflyAnim = null;

    [SerializeField] float noiseScale = 0f;
    [SerializeField] float speed = 0f;

    bool isSkip = false;

    int hashButterflyFLY = 0;
    int hashButterflyIDLE = 0;

    private void Start()
    {
        butterfly.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(item =>
        {
            item.material.SetTexture("_MainTex", butterflyDissolveTex);
            item.material.SetFloat("_NoiseScale", noiseScale);
           butterflyDissolveMats.Add(item.material);
        });

        hashButterflyFLY = Animator.StringToHash("bFly");
        hashButterflyIDLE = Animator.StringToHash("bIdle");
    }

    public void Disappear(Transform destination, System.Action callBack)
    {
        butterfly.position = transform.position;
        butterfly.rotation = Quaternion.Euler(Vector3.zero);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        butterflyDissolveMats.ForEach(item => item.SetFloat("_NoiseStrength", 300));

        butterflyAnim.SetTrigger(hashButterflyFLY);

        StartCoroutine(FlyForDisappear(destination, callBack));
    }
    
    public void SkipButterFlyMove()
    {
        isSkip = true;
    }

    private IEnumerator FlyForDisappear(Transform dest, System.Action callBack)
    {
        float distToDest = Vector2.Distance(dest.position, butterfly.position);

        while (Vector2.Distance(dest.position, butterfly.position) >= 0.01f)
        {
            butterfly.LookAt(dest);
            transform.Translate((dest.position - transform.position).normalized * speed * Time.deltaTime);
            Debug.DrawRay(butterfly.position, (dest.position - butterfly.position), Color.red, 10);

            butterflyDissolveMats.ForEach(item =>
            {
                item.SetFloat("_NoiseStrength", Mathf.Lerp(0, 300, Vector2.Distance(dest.position, butterfly.position) / distToDest));
            });

            if(butterflyDissolveMats[0].GetFloat("_NoiseStrength") <= 40 && distToDest / 2 >= Vector2.Distance(dest.position, butterfly.position))
            {
                ps.Play();
                callBack();
                break;
            }

            if (isSkip)
            {
                butterflyDissolveMats.ForEach(item =>
                {
                    item.SetFloat("_NoiseStrength", 0);
                });

                isSkip = false;
                ps.Play();
                callBack();
                break;
            }

            yield return null;
        };
        yield return null;
    }
}
