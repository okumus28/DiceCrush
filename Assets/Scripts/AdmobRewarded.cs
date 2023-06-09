using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmobRewarded : MonoSingleton<AdmobRewarded>
{
    private RewardedAd rewardedAd;
    [SerializeField] string adUnitId;
    [SerializeField] string gameID;

    Action rewardedAdAction;

    public static int rewardCount = 0;

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => {
            Debug.Log("Initialized");
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            Debug.Log("REKLAM VAR : " + rewardedAd);
            return;
            //rewardedAd.Destroy();
            //rewardedAd = null;
            //Debug.Log("ÖDÜLLÜ REKLAM SİLİNDİ");
        }

        Debug.Log("ÖDÜLLÜ REKLAM YÜKLENİYor");

        var adRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("Ödüllü reklam yüklenirken hata : " + "Error : " + error);
                return;
            }

            Debug.Log("ödüllü reklam yüklendi : " + ad.GetResponseInfo());

            rewardedAd = ad;
            
            ad.OnAdFullScreenContentClosed += RewardedAdClosed;


        });
    }

    public void ShowAd(Action action)
    {
        LoadRewardedAd();
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // Reward the user
                //Debug.Log($"Rewarded ad rewarded the user.Type: {reward.Type}, amount: {reward.Amount}.");
                Debug.Log("Ödüllü reklam gösterildi");
                rewardedAdAction = action;
                rewardedAdAction.Invoke();
                //rewardedAd.OnAdImpressionRecorded += () => { Debug.Log("asdasd"); };
                
                //rewardedAd.OnAdPaid += (AdValue adValue) =>
                //{
                //    Debug.Log(String.Format("Ödüllü reklam {0} {1} ödedi." , adValue.Value , adValue.Precision ));

                //    rewardedAdAction.Invoke();
                //    RegisterReloadHandler(rewardedAd);
                //};
            });
        }
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += RewardedAdClosed;

        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }
    void RewardedAdClosed()
    {
        Debug.Log("Ödüllü reklam kapatıldı.");

        Debug.Log("Ödüllü reklam Siliniyor..........");
        rewardedAd.Destroy();
        rewardedAd = null;

        // Reload the ad so that we can show another as soon as possible.
        LoadRewardedAd();
    }
}
