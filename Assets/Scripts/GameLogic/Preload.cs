using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Preload : MonoBehaviour
{
    public RectTransform tran;

    void Start ()
    {
        StartCoroutine(Cor());
    }



    IEnumerator Cor()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync("New Main", LoadSceneMode.Single);
        AO.allowSceneActivation = false;

        while (AO.progress < 0.9f)
        {
            tran.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -AO.progress * 360.0f));
            yield return null;
        }

        AO.allowSceneActivation = true;
    }
}
