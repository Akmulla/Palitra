using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class CreateAds : MonoBehaviour
{
    public int lvl_ind = 0;
    public int try_ind = 0;


    void ShowAd()
    {
        lvl_ind = 0;
        try_ind = 0;
        
        StartCoroutine(ShowRewardVideo());
    }

    void OnEnable()
    {
        EventManager.StartListening("EndGame", Reload);
        EventManager.StartListening("LvlFinished", LvlPassed);
    }

    void OnDisable()
    {
        EventManager.StopListening("EndGame", Reload);
        EventManager.StopListening("LvlFinished", LvlPassed);
    }

    void LvlPassed()
    {
        lvl_ind++;
        if (lvl_ind==10)
        {
            ShowAd();
        }
    }

    void Reload()
    {
        try_ind++;
        if (try_ind == 10)
        {
            ShowAd();
        }
    }

    IEnumerator ShowRewardVideo()
    {
        GameController.game_controller.Pause();
        yield return new WaitForSecondsRealtime(0.5f);
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            // yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.2f);
        }
        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show("rewardedVideo", options);

        while (Advertisement.isShowing)
        {
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.2f);
        }

        //while (Advertisement.isShowing)
        //{
        //    yield return new WaitForEndOfFrame();
        //}
        GameController.game_controller.Continue();
    }

    public void ShowLifeMenuVideo()
    {
        StartCoroutine(ShowMenuVideo());
    }

    IEnumerator ShowMenuVideo()
    {
        GameController.game_controller.Pause();
        yield return new WaitForSecondsRealtime(0.5f);
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            // yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.2f);
        }
        var options = new ShowOptions { resultCallback = LifeMenuResult };
        Advertisement.Show("rewardedVideo", options);

        while (Advertisement.isShowing)
        {
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.2f);
        }

        //while (Advertisement.isShowing)
        //{
        //    yield return new WaitForEndOfFrame();
        //}
        GameController.game_controller.Continue();
    }

    void LifeMenuResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                Hearts.h.Heart += 2;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
