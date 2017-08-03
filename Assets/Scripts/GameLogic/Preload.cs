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
    bool start = false;

    IEnumerator Start ()
    {
        Application.targetFrameRate = 60;
        sceneLoadTask =SceneManager.LoadSceneAsync("New Main");
        sceneLoadTask.allowSceneActivation = false;
        loadingProgress = 0.0f;
        //progressBar.fillAmount = loadingProgress;
        while (!start)
        {
            yield return null;
        }
        sceneLoadTask.allowSceneActivation = true;
    }

    void Update()
    {
        loadingProgress = sceneLoadTask.progress;
        progressBar.fillAmount = loadingProgress;
        if ((!start)&&(sceneLoadTask.progress>0.89f))
        {
            start = true;
            
            //StartCoroutine(Cor());
        }
            

    }
    IEnumerator Cor()
    {
        yield return new WaitForSeconds(0.5f);
        sceneLoadTask.allowSceneActivation = true;
    }
}
