using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class AdInterstitial : Singleton<AdInterstitial>
{
    [SerializeField]
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    private InterstitialAd _interstitialAd;
    private Action _onAdClosed;

    void Start()
    {
        MobileAds.Initialize(_ => LoadInterstitialAd());
    }

    public void LoadInterstitialAd()
    {
        _interstitialAd?.Destroy();
        _interstitialAd = null;

        Debug.Log("Загрузка межстраничной рекламы.");
        AdRequest adRequest = new AdRequest.Builder().Build();
        InterstitialAd.Load(_adUnitId, adRequest, (ad, error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError($"Ошибка загрузки межстраничной рекламы: {error}");
                return;
            }

            Debug.Log("Межстраничная реклама загружена.");
            _interstitialAd = ad;
            RegisterEventHandlers(_interstitialAd);
        });
    }

    public void ShowInterstitialAd(Action onAdClosed = null)
    {
        if (!SettingsManager.Instance.IsAdEnabled)
        {
            Debug.Log("Реклама выключена.");
            return;
        }

        if (_interstitialAd?.CanShowAd() == true)
        {
            Debug.Log("Показ межстраничной рекламы.");
            _onAdClosed = onAdClosed;
            _interstitialAd.Show();
        }
        else
        {
            Debug.Log("Межстраничная реклама еще не готова.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdPaid += adValue =>
            Debug.Log($"Межстраничная реклама принесла доход: {adValue.Value} {adValue.CurrencyCode}");

        interstitialAd.OnAdFullScreenContentOpened += () =>
            Debug.Log("Межстраничная реклама открыла полноэкранный контент.");

        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Межстраничная реклама закрыла полноэкранный контент.");
            _interstitialAd.Destroy();
            _interstitialAd = null;
            _onAdClosed?.Invoke();
            LoadInterstitialAd(); 
        };

        interstitialAd.OnAdFullScreenContentFailed += error =>
        {
            Debug.LogError($"Ошибка показа межстраничной рекламы: {error}");
            _interstitialAd.Destroy();
            _interstitialAd = null;
            LoadInterstitialAd();
        };
    }
}
