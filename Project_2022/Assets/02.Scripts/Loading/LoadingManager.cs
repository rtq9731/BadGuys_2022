using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class LoadingManager
{
    static string sceneName = "";

    public static void LoadScene(string sceneName, bool isExit)
    {
        DOTween.CompleteAll();
        LoadingSceneManager.LoadScene(sceneName, isExit);
    }
}
