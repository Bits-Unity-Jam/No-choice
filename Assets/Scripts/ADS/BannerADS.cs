using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerADS : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    
    [Header("ID Ad")]
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
   
    [Header("Game ID")]
    [SerializeField] private string _androidGameID;
    [SerializeField] private string _iOSGameID;
    
    string _adUnitId = null;
    private string _gameID;

    void Start()
    {
        #if UNITY_IOS
                _adUnitId = _iOSAdUnitId;
                _gameID = _iOSGameID;
        #elif UNITY_ANDROID
                _adUnitId = _androidAdUnitId;
                _gameID = _androidGameID;
        #endif

        Advertisement.Banner.SetPosition(_bannerPosition);

        // Set the initialization listener
        Advertisement.Initialize(_gameID, testMode: true, initializationListener: this);

        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner Load");

        ShowBannerAd();
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adUnitId, options);
    }

    void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { Debug.Log("Banner Clicked"); }
    void OnBannerShown() { Debug.Log("Banner Shown"); }
    void OnBannerHidden() { Debug.Log("Banner Hidden"); }

    // Implementation of IUnityAdsInitializationListener interface
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed with error {error}: {message}");
    }
}
