using UnityEngine;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using System;

public class GoogleMobileAdsService : IGoogleMobileAdsService
{
    private RewardedAd _rewardedAd;
    private string rewardID = "ca-app-pub-6749320954829314/5690333467";

    public void LoadReward()
    {
        MobileAds.Initialize(initStatus =>
        {
            LoadApp();
        });
    }
    public void ShowRewardAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
        else
        {
            LoadReward();
        }
    }
    
    private void LoadApp()
    {
        _rewardedAd = new RewardedAd(rewardID);
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }
}
