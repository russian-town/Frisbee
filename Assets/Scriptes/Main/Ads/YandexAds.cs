using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexAds : MonoBehaviour
{
    public Action OpenCallback;
    public Action RewardedCallback;
    public Action CloseCallback;
    public Action<string> ErrorCallback;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();
    }

    public void ShowVideo()
    {
        VideoAd.Show(OpenCallback, RewardedCallback, CloseCallback, ErrorCallback);
    }

    public void ShowInterstitial()
    {
        InterstitialAd.Show();
    }
}
