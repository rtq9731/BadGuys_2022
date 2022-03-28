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

    void Start()
    {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainStage_Stage" + LoadingSceneManager.sceneColor));
            blindImageBlack.gameObject.SetActive(false);
            blindImageWhite.DOFade(0f, 1f).OnComplete(() =>
            {
                SceneManager.UnloadSceneAsync("PictureMoveEffectScene");
            });
    }
}
