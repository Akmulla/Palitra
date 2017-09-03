using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preload : MonoBehaviour
{
    AsyncOperation sceneLoadTask;
    public Image progressBar;

    void Start ()
    {
        Application.targetFrameRate = 60;
        sceneLoadTask = SceneManager.LoadSceneAsync("New Main");
        sceneLoadTask.allowSceneActivation = false;
        progressBar.fillAmount = 0.2f;
    }

    void Update()
    {
        progressBar.fillAmount = sceneLoadTask.progress;
        
        if (sceneLoadTask.progress>0.89f)
        {
            sceneLoadTask.allowSceneActivation = true;
        }
    }
}
