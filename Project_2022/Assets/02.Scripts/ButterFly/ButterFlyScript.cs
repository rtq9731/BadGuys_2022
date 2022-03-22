using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButterFlyScript : MonoBehaviour
{
    [SerializeField] Transform butterfly = null;
    [SerializeField] Texture2D butterflyDissolveTex = null;
    List<Material> butterflyDissolveMats = new List<Material>();

    [SerializeField] Animator butterflyAnim = null;

    [SerializeField] float speed = 0f;

    int hashButterflyFLY = 0;
    int hashButterflyIDLE = 0;

    private void Start()
    {
        butterfly.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(item =>
        {
            item.material.SetTexture("_MainTex", butterflyDissolveTex);
            butterflyDissolveMats.Add(item.material);
        });

        hashButterflyFLY = Animator.StringToHash("bFly");
        hashButterflyIDLE = Animator.StringToHash("bIdle");
    }

    public void Disappear(Transform destination, System.Action callBack)
    {
        butterflyDissolveMats.ForEach(item => item.SetFloat("_NoiseStrength", 300f));
        Sequence seq = DOTween.Sequence();
        butterflyAnim.SetTrigger(hashButterflyFLY);

        StartCoroutine(FlyForDisappear(destination, callBack));
        butterflyDissolveMats.ForEach(item => seq.Join(item.DOFloat(0, "_NoiseStrength", 7f)).SetEase(Ease.InCubic));
    }

    private IEnumerator FlyForDisappear(Transform dest, System.Action callBack)
    {
        while (Vector2.Distance(dest.position, transform.position) >= 0.01f)
        {
            transform.Translate((dest.position - transform.position).normalized * speed * Time.deltaTime);
            butterfly.LookAt(dest);

            yield return null;
        }
        callBack();
    }
}
