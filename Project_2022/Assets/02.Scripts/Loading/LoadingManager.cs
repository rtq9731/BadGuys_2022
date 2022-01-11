using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class LoadingManager
{
    static string sceneName = "";

    public static void LoadScene(string sceneName)
    {
        LoadingManager.sceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
        SceneManager.sceneLoaded += OnLoadScene;
    }

    public static void OnLoadScene(Scene scene, LoadSceneMode sceneLoadMode)
    {
        if (scene.name == "LoadingScene")
        {
            GameObject.FindObjectOfType<LoadingSceneManager>().SetLoading(sceneName);
        }

        DOTween.CompleteAll();
    }
}
