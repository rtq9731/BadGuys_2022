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
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        DOTween.CompleteAll();
    }

    public static void OnSceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
    {
        if (scene.name == "LoadingScene")
        {
            GameObject.FindObjectOfType<LoadingSceneManager>().SetLoading(sceneName);
        }
    }
}
