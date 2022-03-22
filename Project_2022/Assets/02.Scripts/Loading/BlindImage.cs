using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BlindImage : MonoBehaviour
{
    public string sceneColor;

    [SerializeField]
    private Image blindImageWhite;
    [SerializeField]
    private Image blindImageBlack;

    private void Awake()
    {
        
    }

    void Start()
    {
        blindImageBlack.gameObject.SetActive(false);
        blindImageWhite.DOFade(0f, 1f).OnComplete(() =>
        {
            SceneManager.UnloadSceneAsync("PictureMoveEffectScene");
        });
    }

    
    void Update()
    {
        
    }
}
