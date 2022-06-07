using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseHPSystem : MonoBehaviour
{
    [SerializeField] Image handCuffImage = null;
    [SerializeField] StageReStart restart;
    [SerializeField] float maxHp = 100;
    [SerializeField] float damage = 1f;

    float hp = 100f;
    bool over = false;

    public float HP
    {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0, 100); }
    }

    private void Awake()
    {
        hp = maxHp;
    }

    private void Update()
    {
        if (hp <= 0 && !over)
        {
            over = true;
            restart.Detection();
        }
            
        
        if(!GameManager.Instance.IsPause)
            hp -= Time.deltaTime * damage;

        handCuffImage.color = new Color(0.5f, Mathf.Lerp(0, 1, hp / maxHp), 0);
        handCuffImage.fillAmount = Mathf.Lerp(0, 1, hp / maxHp);
    }

    public static void PlusHP(float heal)
    {
        FindObjectOfType<ChaseHPSystem>().HP += heal;
    }
}
