using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseHPSystem : MonoBehaviour
{
    [SerializeField] Image handCuffImage = null;

    [SerializeField] float maxHp = 100;
    [SerializeField] float damage = 1f;

    float hp = 100f;

    private void Awake()
    {
        hp = maxHp;
    }

    private void Update()
    {
        if(!GameManager.Instance.IsPause)
            hp -= Time.deltaTime * damage;

        handCuffImage.color = new Color(0.5f, Mathf.Lerp(0, 1, hp / maxHp), 0);
        handCuffImage.fillAmount = Mathf.Lerp(0, 1, hp / maxHp);
    }

    public static void PlusHP(float heal)
    {
        FindObjectOfType<ChaseHPSystem>().hp += heal;
    }
}
