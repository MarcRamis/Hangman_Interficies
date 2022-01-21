using UnityEngine;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using System;

public class GoogleMobileAdsService : IGoogleMobileAdsService
{
    private RewardedAd _rewardedAd;
    private string rewardID = "ca-app-pub-6749320954829314/5690333467";

    public async Task LoadReward()
    {
        MobileAds.Initialize(initStatus =>
        {
            LoadApp();
        });

        await Task.Delay(TimeSpan.FromSeconds(1));
    }
    public async void ShowRewardAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
        else
        {
            await LoadReward();
        }
    }
    
    private void LoadApp()
    {
        _rewardedAd = new RewardedAd(rewardID);
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }
}
