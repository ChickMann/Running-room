using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using TMPro;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;
using static UnityEngine.Random;
using Unity.VisualScripting;
using NUnit.Framework.Interfaces;
using static GameManager;

public class AdmodAdsScript : MonoBehaviour
{
    [SerializeField] private string appID = "";




    string bannerId = "ca-app-pub-8836125761137733/6547919489";
    string interId = "ca-app-pub-8836125761137733/2417102787";
    string rewardId = "ca-app-pub-8836125761137733/8544076178";
    //string nativeId = "ca-app-pub-8836125761137733/8089066012";
    string openId = "ca-app-pub-8836125761137733/6644671148";

    BannerView bannerView;
    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;
    //NativeAd nativeAd;
    AppOpenAd openAd;


    private void Awake()
    {

        // Use the AppStateEventNotifier to listen to application open/close events.
        // This is used to launch the loaded ad when we open the app.
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }
    private void OnDestroy()
    {
        // Always unlisten to events when complete.
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

        //InvokeRepeating("TimeCounter", 0, 1);
        //ShowTime();
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

    }

    #region extra

    //void GrantCoins(int coin)
    //{
    //    int crrCoins = PlayerPrefs.GetInt("totalCoins");
    //    crrCoins += coin;
    //    PlayerPrefs.SetInt("totalCoins", crrCoins);
    //    ShowCoin();
    //}
    //void ShowCoin()
    //{
    //    totalCoinTxt.text = "Coin: " + PlayerPrefs.GetInt("totalCoins").ToString();
    //}

    //void TimeCounter()
    //{

    //    currentTime = PlayerPrefs.GetInt("timeText");
    //    isCounting = currentTime > 0 ? true : false;
    //    if (isCounting)
    //    {
    //        currentTime--;
    //    }
    //    else
    //    {
    //        CancelInvoke("TimeCounter");
    //    }
    //    PlayerPrefs.SetInt("timeText", currentTime);
    //    ShowTime();
    //}
    //void ShowTime()
    //{
    //    if (isCounting)
    //    {
    //        timeText.SetText($"ads({currentTime})");
    //    }
    //    else
    //    {
    //        timeText.SetText($"ads");
    //    }

    //}

    #endregion
    #region Banner

    public void LoadBannerAD()
    {
        //create a banner
        CreateBannerView();

        //listen to banner view
        ListenToBannerViews();
        //load then banner
        if (bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("Unity-admod");
        print("Loading Banner ad!!");
        bannerView.LoadAd(adRequest);
    }
    void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerAD();
        }
        bannerView = new BannerView(bannerId, AdSize.IABBanner, AdPosition.Bottom);
    }
    void ListenToBannerViews()
    {
        // Raised when an ad is loaded into the banner view.
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }
    public void DestroyBannerAD()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            bannerView.Destroy();
            bannerView = null;
        }
    }
    #endregion
    #region interstitial

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(interId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(ad);
            });
    }
    public void ShowInterstitialAd()
    {

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }

    }
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion
    #region Reward

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(rewardId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }
    public void ShowRewardedAd()
    {
        LoadRewardedAd();

        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //GrantCoins(10);
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }


    }
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };

    }

    #endregion
    //#region Native

    //[SerializeField] private Image img;
    //public void RequestNativeAd()
    //{
    //    AdLoader adLoader = new AdLoader.Builder(nativeId).ForNativeAd().Build();
    //    adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
    //    adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
    //    adLoader.LoadAd(new AdRequest.Builder().Build());
    //}

    //private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    //{
    //    print(" Fail native ad!!");
    //}

    //private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    //{
    //    Debug.Log("Native ad loaded.");
    //    this.nativeAd = args.nativeAd;
    //    Texture2D texture2D = this.nativeAd.GetIconTexture();
    //    Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * .5f);
    //    img.sprite = sprite;
    //}

    //#endregion
    #region Open

    public bool IsAdAvailable
    {
        get
        {
            return openAd != null;
        }
    }
    private void OnAppStateChanged(AppState state)
    {

        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            if (IsAdAvailable)
            {
                ShowAppOpenAd();
            }
        }

    }

    public void ShowAppOpenAd()
    {
        LoadAppOpenAd();
        if (openAd != null && openAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            openAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
        }
    }


    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (openAd != null)
        {
            openAd.Destroy();
            openAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(openId, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                openAd = ad;
                RegisterEventHandlers(ad);

            });
    }
    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion
}
