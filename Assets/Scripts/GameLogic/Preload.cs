using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preload : MonoBehaviour
{
    AsyncOperation sceneLoadTask;
    public Image progressBar;
    float loadingProgress = 0.0f;

    void Start ()
    {
        Application.targetFrameRate = 60;
        sceneLoadTask = SceneManager.LoadSceneAsync("New Main");
        sceneLoadTask.allowSceneActivation = false;
        loadingProgress = 0.0f;
        progressBar.fillAmount = loadingProgress;
    }

    void Update()
    {
        loadingProgress = sceneLoadTask.progress;
        progressBar.fillAmount = loadingProgress;
        
        if (sceneLoadTask.progress>0.89f)
        {
            sceneLoadTask.allowSceneActivation = true;
        }
    }
}
