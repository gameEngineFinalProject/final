using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Loader {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum Scene
    {
        MainMenu,
        GameScene,
        LoadScene
    }
    private static Scene targetScene;
    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadScene.ToString());

    }
    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
    }
