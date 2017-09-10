using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Preload : MonoBehaviour
{
    AsyncOperation sceneLoadTask;
    public Image progressBar;
    //public Text text;

    void Start ()
    {
        //Application.targetFrameRate = 60;
        //sceneLoadTask = SceneManager.LoadSceneAsync("New Main",LoadSceneMode.Additive);
        //sceneLoadTask.allowSceneActivation = false;
        //progressBar.fillAmount = 0.2f;

        StartCoroutine(Cor());
    }

    void Update()
    {
        //progressBar.fillAmount = sceneLoadTask.progress;
        
        //if (sceneLoadTask.progress>0.89f)
        //{
        //    sceneLoadTask.allowSceneActivation = true;
        //}
    }

    IEnumerator Cor()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync("New Main", LoadSceneMode.Single);
        AO.allowSceneActivation = false;
        while (AO.progress < 0.9f)
        {
           // text.text = AO.progress.ToString();
            progressBar.fillAmount = AO.progress;
            yield return null;
        }

        //Fade the loading screen out here

        AO.allowSceneActivation = true;
    }
}
