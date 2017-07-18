using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preload : MonoBehaviour
{
    AsyncOperation sceneLoadTask;
    public Image progressBar;
    float loadingProgress;

    void Start ()
    {
        sceneLoadTask=SceneManager.LoadSceneAsync("New Main");
        loadingProgress = 0.0f;
        progressBar.fillAmount = loadingProgress;
    }

    void Update()
    {
        loadingProgress = sceneLoadTask.progress;
        progressBar.fillAmount = loadingProgress;
    }
}
