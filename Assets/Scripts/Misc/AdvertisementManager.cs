using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Properties for each platform
    private static string GameID
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer: return "4637782";
                case RuntimePlatform.Android: return "4637783";
            }
            return "";
        }
    }
    private static string RewardedAd
    {
        get
        {
            switch (Application.platform)
            {                
                case RuntimePlatform.IPhonePlayer: return "Rewarded_IOS";
                case RuntimePlatform.Android:      return "Rewarded_Android";
            }            
            return "Rewarded_Android";
        }
    }
    private static string InterstitialAd
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer: return "Interstitial_iOS";
                case RuntimePlatform.Android: return "Interstitial_Android";
            }
            return "";
        }
    }

    public bool testMode = true;
    public System.Action OnAdFinished = null;
    private bool adsEnabled = true;
    // Unity Events
    void Awake()
    {
        if (GameID != "")
        {
            Advertisement.Initialize(GameID, testMode, this);
        }
        else
        {
            adsEnabled = false;
            AdComplete();
        }
    }
    void Start()
    {
        if (adsEnabled)
            LoadAd();
        else
            AdComplete();
    }

    // Result of everything
    private void AdComplete()
    {
        OnAdFinished?.Invoke();
        OnAdFinished = null;
    }

    // IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError(message);
        AdComplete();
    }

    // IUnityAdsLoadListener - From unity integration manual
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + InterstitialAd);
        Advertisement.Load(InterstitialAd, this);
    }
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + InterstitialAd);
        Advertisement.Show(InterstitialAd, this);
    }
    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        ShowAd();
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        AdComplete();
    }

    //IUnityAdsShowListener - From unity integration manual
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        adsEnabled = false;
        AdComplete();
    }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) 
    {
        AdComplete();
    }
}
