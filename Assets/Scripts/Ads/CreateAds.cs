using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class CreateAds : MonoBehaviour
{
    //public int lvl_ind = 0;
    //public int try_ind = 0;


    void ShowAd()
    {
        //lvl_ind = 0;
        //try_ind = 0;
        
        StartCoroutine(ShowRewardVideo());
    }

    //void OnEnable()
    //{
    //    EventManager.StartListening("EndGame", Reload);
    //    EventManager.StartListening("LvlFinished", LvlPassed);
    //}

    //void OnDisable()
    //{
    //    EventManager.StopListening("EndGame", Reload);
    //    EventManager.StopListening("LvlFinished", LvlPassed);
    //}

    //void LvlPassed()
    //{
    //    lvl_ind++;
    //    if (lvl_ind==10)
    //    {
    //        ShowAd();
    //    }
    //}

    //void Reload()
    //{
    //    try_ind++;
    //    if (try_ind == 10)
    //    {
    //        ShowAd();
    //    }
    //}

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
        int t = 0;
        while ((!Advertisement.IsReady("rewardedVideo")&&(t<20)))
        {
            // yield return new WaitForEndOfFrame();
            t++;
            yield return new WaitForSecondsRealtime(0.2f);
        }


        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = LifeMenuResult };
            Advertisement.Show("rewardedVideo", options);

            while (Advertisement.isShowing)
            {
                //yield return new WaitForEndOfFrame();
                yield return new WaitForSecondsRealtime(0.2f);
            }
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

    public void ShowContinueVideo()
    {
        StartCoroutine(ContinueVideo());
    }

    IEnumerator ContinueVideo()
    {
       
        GameController.game_controller.continued = true;
        GameController.game_controller.Pause();
        yield return new WaitForSecondsRealtime(0.5f);
        int t = 0;
        while ((!Advertisement.IsReady("rewardedVideo") && (t < 20)))
        {
            t++;
            yield return new WaitForSecondsRealtime(0.2f);
        }


        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleContinueResult };
            Advertisement.Show("rewardedVideo", options);

            while (Advertisement.isShowing)
            {
                //yield return new WaitForEndOfFrame();
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }

        GameController.game_controller.Continue();
    }

    void HandleContinueResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                GameController.game_controller.ResumeForBanner();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                GameController.game_controller.Continue();
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                GameController.game_controller.Continue();
                break;
            default:
                GameController.game_controller.Continue();
                break;
        }
    }




    public void ShowRubinMenuVideo()
    {
        StartCoroutine(RubinMenuVideo());
    }

    IEnumerator RubinMenuVideo()
    {
        GameController.game_controller.Pause();
        yield return new WaitForSecondsRealtime(0.5f);
        int t = 0;
        while ((!Advertisement.IsReady("rewardedVideo") && (t < 20)))
        {
            t++;
            yield return new WaitForSecondsRealtime(0.2f);
        }


        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = RubinMenuResult };
            Advertisement.Show("rewardedVideo", options);

            while (Advertisement.isShowing)
            {
                //yield return new WaitForEndOfFrame();
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }

        GameController.game_controller.Continue();
    }

    void RubinMenuResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                GlobalScore.global_score.Score += 200;
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
