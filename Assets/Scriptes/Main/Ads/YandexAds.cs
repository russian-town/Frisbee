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

    public void ShowVideo()
    {
        VideoAd.Show(OpenCallback, RewardedCallback, CloseCallback, ErrorCallback);
    }

    public void ShowInterstitial()
    {
        InterstitialAd.Show();
    }
}
