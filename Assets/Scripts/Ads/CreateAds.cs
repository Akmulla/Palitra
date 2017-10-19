using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class CreateAds : MonoBehaviour
{
    public int lvlInd;
    public int tryInd;


    void ShowAd()
    {
        lvlInd = 0;
        tryInd = 0;
        
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
        lvlInd++;
        if (lvlInd==10)
        {
            ShowAd();
        }
    }

    void Reload()
    {
        tryInd++;
        if (tryInd == 10)
        {
            ShowAd();
        }
    }

    IEnumerator ShowRewardVideo()
    {
        GameController.gameController.Pause();
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
        GameController.gameController.Continue();
    }

    public void ShowLifeMenuVideo()
    {
        StartCoroutine(ShowMenuVideo());
    }

    IEnumerator ShowMenuVideo()
    {
        GameController.gameController.Pause();
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
        GameController.gameController.Continue();
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
        GameController.gameController.Pause();
        yield return new WaitForSecondsRealtime(0.5f);
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            // yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.2f);
        }
        var options = new ShowOptions { resultCallback = HandleContinueResult };
        Advertisement.Show("rewardedVideo", options);

        while (Advertisement.isShowing)
        {
            //print("showing");
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.2f);
        }

        //while (Advertisement.isShowing)
        //{
        //    yield return new WaitForEndOfFrame();
        //}

        //GameController.game_controller.Continue();
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
                GameController.gameController.ResumeForBanner();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                GameController.gameController.Continue();
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                GameController.gameController.Continue();
                break;
            default:
                GameController.gameController.Continue();
                break;
        }
    }
}
