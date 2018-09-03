using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Preload : MonoBehaviour
{
    public RectTransform tran;
    //[SerializeField] AAT_Ads aatAds;
    const string multiSizeId = "MultiSizePlacement";

    void Start ()
    {

        StartCoroutine(Cor());
        
        
    }



    IEnumerator Cor()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync("New Main", LoadSceneMode.Single);
        AO.allowSceneActivation = false;
        float progress = 0.0f;
        //aatAds.ShowPromo();
        //AATKitBinding.InitWithTestMode(this.gameObject.name, 49);

        //AATKitBinding.CreatePlacement(multiSizeId, AATKitBinding.PlacementSize.MultiSizeBanner);
        //AATKitBinding.StartPlacementAutoReload(multiSizeId);
        AATKitBinding.ShowPromo(true);

        bool a=false;
        float t = Time.time;
        while ((!a)&&(Time.time - t <5.0f))
        {
            //a = AATKitBinding.ShowPromo(true);

           // print(a);
            yield return null;
        }

        string s = a ?"success":"failed";
        //print(s);
        while (AO.progress < 0.9f)
        {
            progress = AO.progress > 0.2f ? AO.progress - 0.2f : AO.progress;
            tran.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -progress * 360.0f));
            yield return null;
        }
        while ((!a) && (Time.time - t < 5.0f))
        {
            //a = AATKitBinding.ShowPromo(true);

            // print(a);
            yield return null;
        }
        AO.allowSceneActivation = true;
    }
}
