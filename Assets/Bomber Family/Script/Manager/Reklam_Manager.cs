using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

[Serializable]
public class ReceiptData
{
    public string payload;
    public string store;
    public string transactionID;
}
[Serializable]
public class Payload
{
    public string json;
    public string signature;
    public PaymentData paymentData;
}
[Serializable]
public class PaymentData
{
    public string orderId;
    public string productId;
    public string packageName;
    public string purchaseToken;
    public int purchaseState;
    public int quantity;
    public long purchaseTime;
    public bool acknowledged;
}
public class Reklam_Manager : Singletion<Reklam_Manager>
{
    [Header("Purchase Buttons")]
    [SerializeField] private Button goldButton100;
    [SerializeField] private Button goldButton200;
    [SerializeField] private Button goldButton300;
    [SerializeField] private Button goldButton500;
    [SerializeField] private Button goldButton1500;
    [SerializeField] private Button goldButton2500;
    [SerializeField] private Button goldButton5000;
    [SerializeField] private Button goldButton7500;
    [SerializeField] private Button goldButton10000;

    [Header("Ads")]
    // Reklam kimliği : ca-app-pub-1398478089736122~7784829166
    // InterRewarded : ca-app-pub-1398478089736122/5005515368
    // Rewarded : ca-app-pub-1398478089736122/3620281509
    //https://play.google.com/store/apps/details?id=com.Kimex.WonderfulPuzzles&hl=tr&gl=US
    [SerializeField] private bool isTest = true;
    [SerializeField] private List<Button> reklamButtons = new List<Button>();

    private const string _adInterAdsId = "ca-app-pub-1398478089736122/5005515368";
    private const string _adRewardAdsId = "ca-app-pub-1398478089736122/3620281509";

    private const string _adTestInterAdsId = "ca-app-pub-3940256099942544/5354046379";
    private const string _adTestRewardAdsId = "ca-app-pub-3940256099942544/5224354917";

    private RewardedAd _rewardedAd;
    private RewardedInterstitialAd _rewardedInterstitialAd;

    private float interLoadingTime;
    private float rewardedLoadingTime;

    private int reklamAmount;

    private const string gold100 = "gold100";       // 1
    private const string gold200 = "gold200";       // 2
    private const string gold300 = "gold300";       // 3
    private const string gold500 = "gold500";       // 
    private const string gold1500 = "gold1500";     // 
    private const string gold2500 = "gold2500";     // 
    private const string gold5000 = "gold5000";     // 
    private const string gold7500 = "gold7500";     // 
    private const string gold10000 = "gold10000";   // 

    private void Start()
    {
        InterLoadAd();
        RewardLoadAd();
    }
    public void AddReklamButton(Button buton)
    {
        reklamButtons.Add(buton);
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Gold-Container -> Panel-Shop-Gold-Parent -> Panel-Button-Gold-X -> Button-Buy-Gold-X'lere atandı
    public void OnPurchaseFailed(Product purchase, PurchaseFailureDescription purchaseFailureDescription)
    {
        string goldName = "";
        if (purchase.definition.id == gold100)
        {
            goldName = "100 Gold";
        }
        else if (purchase.definition.id == gold200)
        {
            goldName = "200 Gold";
        }
        else if (purchase.definition.id == gold300)
        {
            goldName = "300 Gold";
        }
        else if (purchase.definition.id == gold500)
        {
            goldName = "500 Gold";
        }
        else if (purchase.definition.id == gold1500)
        {
            goldName = "1500 Gold";
        }
        else if (purchase.definition.id == gold2500)
        {
            goldName = "2500 Gold";
        }
        else if (purchase.definition.id == gold5000)
        {
            goldName = "5000 Gold";
        }
        else if (purchase.definition.id == gold7500)
        {
            goldName = "7500 Gold";
        }
        else if (purchase.definition.id == gold10000)
        {
            goldName = "10000 Gold";
        }
        Warning_Manager.Instance.ShowMessage(goldName + " purchase was cancelled.", 2);
    }
    public void UpdateButtonForPurchasePrice(Product purchase, Button buton)
    {
        TextMeshProUGUI butonText = buton.GetComponentInChildren<TextMeshProUGUI>();
        butonText.text = purchase.metadata.localizedPrice + " " + purchase.metadata.isoCurrencyCode;
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Gold-Container -> Panel-Shop-Gold-Parent -> Panel-Button-Gold-X -> Button-Buy-Gold-X'lere atandı
    public void OnPurchaseFetched(Product purchase)
    {
        if (purchase.definition.id == gold100)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton100);
        }
        else if (purchase.definition.id == gold200)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton200);
        }
        else if (purchase.definition.id == gold300)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton300);
        }
        else if (purchase.definition.id == gold500)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton500);
        }
        else if (purchase.definition.id == gold1500)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton1500);
        }
        else if (purchase.definition.id == gold2500)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton2500);
        }
        else if (purchase.definition.id == gold5000)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton5000);
        }
        else if (purchase.definition.id == gold7500)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton7500);
        }
        else if (purchase.definition.id == gold10000)
        {
            UpdateButtonForPurchasePrice(purchase, goldButton10000);
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Gold-Container -> Panel-Shop-Gold-Parent -> Panel-Button-Gold-X -> Button-Buy-Gold-X'lere atandı
    public void OnPurchaseComplete(Product purchase)
    {
        int quantity = BuyGoldQuantity(purchase);
        if (purchase.definition.id == gold100)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(100);
            }
        }
        else if (purchase.definition.id == gold200)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(200);
            }
        }
        else if (purchase.definition.id == gold300)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(300);
            }
        }
        else if (purchase.definition.id == gold500)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(500);
            }
        }
        else if (purchase.definition.id == gold1500)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(1500);
            }
        }
        else if (purchase.definition.id == gold2500)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(2500);
            }
        }
        else if (purchase.definition.id == gold5000)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(5000);
            }
        }
        else if (purchase.definition.id == gold7500)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(7500);
            }
        }
        else if (purchase.definition.id == gold10000)
        {
            for (int e = 0; e < quantity; e++)
            {
                Canvas_Manager.Instance.SetGoldSmooth(10000);
            }
        }
    }
    public int BuyGoldQuantity(Product purchase)
    {
        int quantity = 1;
        if (purchase.hasReceipt)
        {
            var receipt = purchase.receipt;
            ReceiptData data = JsonUtility.FromJson<ReceiptData>(receipt);
            if (data.store != "fake")
            {
                Payload payload = JsonUtility.FromJson<Payload>(data.payload);
                PaymentData paymentData = JsonUtility.FromJson<PaymentData>(payload.json);
                quantity = paymentData.quantity;
            }
        }
        return quantity;
    }
    public void ShowInterReklam()
    {
        reklamAmount++;
        if (reklamAmount >= 5)
        {
            InterShowAd();
            reklamAmount = 0;
        }
    }
    /// <summary>
    /// Shows the ad.
    /// </summary>
    [ContextMenu("Show Inter Reklam")]
    public void InterShowAd()
    {
        if (_rewardedInterstitialAd == null)
        {
            // Reklamın yüklenmesi çok uzun sürmüş, silip tekrar dene.
            InterLoadAd();
        }
        else
        {
            if (_rewardedInterstitialAd.CanShowAd())
            {
                _rewardedInterstitialAd.Show((Reward reward) =>
                {
                    InterLoadAd();
                    interLoadingTime = Time.time;
                    //Debug.LogWarning("Inter Reklam gösterildi." + reward.Amount);
                });
            }
            else
            {
                if (Time.time - interLoadingTime > 10)
                {
                    // Reklamın yüklenmesi çok uzun sürmüş, silip tekrar dene.
                    InterLoadAd();
                }
            }
        }
    }
    /// <summary>
    /// Shows the ad.
    /// </summary>
    [ContextMenu("Show Reward Reklam")]
    public void RewardShowAd(Action action)
    {
        if (_rewardedAd == null)
        {
            if (Time.time - rewardedLoadingTime > 10)
            {
                RewardLoadAd();
            }
        }
        else
        {
            if (_rewardedAd.CanShowAd())
            {
                for (int e = 0; e < reklamButtons.Count; e++)
                {
                    reklamButtons[e].interactable = false;
                }
                _rewardedAd.Show((Reward reward) =>
                {
                    action?.Invoke();
                    //Debug.Log("Rewarded reklam gösterildi.");
                    rewardedLoadingTime = Time.time;
                    RewardLoadAd();
                });
            }
            else
            {
                if (Time.time - rewardedLoadingTime > 10)
                {
                    RewardLoadAd();
                }
            }
        }
    }
    #region Inter
    /// <summary>
    /// Loads the ad.
    /// </summary>
    public void InterLoadAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedInterstitialAd != null)
        {
            InterDestroyAd();
        }
        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedInterstitialAd.Load(isTest ? _adTestInterAdsId : _adInterAdsId, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // If the operation failed with a reason.
                if (error != null)
                {
                    return;
                }
                // If the operation failed for unknown reasons.
                // This is an unexpexted error, please report this bug if it happens.
                if (ad == null)
                {
                    return;
                }

                // The operation completed successfully.
                //Debug.LogWarning("Inter Reklam yüklendi.");
                _rewardedInterstitialAd = ad;

                // Register to ad events to extend functionality.
                InterRegisterEventHandlers(ad);
            });
    }
    /// <summary>
    /// Destroys the ad.
    /// </summary>
    public void InterDestroyAd()
    {
        if (_rewardedInterstitialAd != null)
        {
            _rewardedInterstitialAd.Destroy();
            _rewardedInterstitialAd = null;
        }
    }
    /// <summary>
    /// Logs the ResponseInfo.
    /// </summary>
    public void InterLogResponseInfo()
    {
        if (_rewardedInterstitialAd != null)
        {
            var responseInfo = _rewardedInterstitialAd.GetResponseInfo();
            Debug.Log(responseInfo);
        }
    }
    protected void InterRegisterEventHandlers(RewardedInterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(String.Format("Rewarded interstitial ad paid {0} {1}.",
            //    adValue.Value,
            //    adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Rewarded interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Rewarded interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Rewarded interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded interstitial ad failed to open full screen content" +
            //               " with error : " + error);
        };
    }
    #endregion

    #region Reward
    /// <summary>
    /// Loads the ad.
    /// </summary>
    public void RewardLoadAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            RewardDestroyAd();
        }
        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(isTest ? _adTestRewardAdsId : _adRewardAdsId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                //Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                //Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            //Debug.LogWarning("Rewarded reklam yüklendi.");
            _rewardedAd = ad;
            for (int e = 0; e < reklamButtons.Count; e++)
            {
                reklamButtons[e].interactable = true;
            }

            // Register to ad events to extend functionality.
            RewardRegisterEventHandlers(ad);
        });
    }
    /// <summary>
    /// Destroys the ad.
    /// </summary>
    public void RewardDestroyAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
    }
    /// <summary>
    /// Logs the ResponseInfo.
    /// </summary>
    public void RewardLogResponseInfo()
    {
        if (_rewardedAd != null)
        {
            var responseInfo = _rewardedAd.GetResponseInfo();
        }
    }
    private void RewardRegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
            //    adValue.Value,
            //    adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
        };
        // Raised when the ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content with error : "
            //    + error);
        };
    }
    #endregion
}