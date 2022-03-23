using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BlindImage : MonoBehaviour
{

    [SerializeField]
    private Image blindImageWhite;
    [SerializeField]
    private Image blindImageBlack;

    private void Awake()
    {
        
    }

    void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainStage_Stage" + LoadingSceneManager.sceneColor));
        SceneManager.UnloadSceneAsync("MainStage_stage1");
        blindImageBlack.gameObject.SetActive(false);
        blindImageWhite.DOFade(0f, 1f).OnComplete(() =>
        {
            SceneManager.UnloadSceneAsync("PictureMoveEffectScene");
        });
    }

    
    
}
