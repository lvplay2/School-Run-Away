using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class Utils
	{
		public const string AdListenerClassName = "com.google.android.gms.ads.AdListener";

		public const string AdRequestClassName = "com.google.android.gms.ads.AdRequest";

		public const string AdRequestBuilderClassName = "com.google.android.gms.ads.AdRequest$Builder";

		public const string AdSizeClassName = "com.google.android.gms.ads.AdSize";

		public const string AdMobExtrasClassName = "com.google.android.gms.ads.mediation.admob.AdMobExtras";

		public const string PlayStorePurchaseListenerClassName = "com.google.android.gms.ads.purchase.PlayStorePurchaseListener";

		public const string InAppPurchaseListenerClassName = "com.google.android.gms.ads.purchase.InAppPurchaseListener";

		public const string BannerViewClassName = "com.google.unity.ads.Banner";

		public const string InterstitialClassName = "com.google.unity.ads.Interstitial";

		public const string RewardBasedVideoClassName = "com.google.unity.ads.RewardBasedVideo";

		public const string NativeExpressAdViewClassName = "com.google.unity.ads.NativeExpressAd";

		public const string NativeAdLoaderClassName = "com.google.unity.ads.NativeAdLoader";

		public const string UnityAdListenerClassName = "com.google.unity.ads.UnityAdListener";

		public const string UnityRewardBasedVideoAdListenerClassName = "com.google.unity.ads.UnityRewardBasedVideoAdListener";

		public const string UnityCustomNativeAdListener = "com.google.unity.ads.UnityCustomNativeAdListener";

		public const string PluginUtilsClassName = "com.google.unity.ads.PluginUtils";

		public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

		public const string BundleClassName = "android.os.Bundle";

		public const string DateClassName = "java.util.Date";

		public static AndroidJavaObject GetAdSizeJavaObject(AdSize adSize)
		{
			if (adSize.IsSmartBanner)
			{
				return new AndroidJavaClass("com.google.android.gms.ads.AdSize").GetStatic<AndroidJavaObject>("SMART_BANNER");
			}
			return new AndroidJavaObject("com.google.android.gms.ads.AdSize", adSize.Width, adSize.Height);
		}

		public static AndroidJavaObject GetAdRequestJavaObject(AdRequest request)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.android.gms.ads.AdRequest$Builder");
			foreach (string keyword in request.Keywords)
			{
				androidJavaObject.Call<AndroidJavaObject>("addKeyword", new object[1] { keyword });
			}
			foreach (string testDevice in request.TestDevices)
			{
				if (testDevice == "SIMULATOR")
				{
					string @static = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<string>("DEVICE_ID_EMULATOR");
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[1] { @static });
				}
				else
				{
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[1] { testDevice });
				}
			}
			if (request.Birthday.HasValue)
			{
				DateTime valueOrDefault = request.Birthday.GetValueOrDefault();
				AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.util.Date", valueOrDefault.Year, valueOrDefault.Month, valueOrDefault.Day);
				androidJavaObject.Call<AndroidJavaObject>("setBirthday", new object[1] { androidJavaObject2 });
			}
			if (request.Gender.HasValue)
			{
				int? num = null;
				switch (request.Gender.GetValueOrDefault())
				{
				case Gender.Unknown:
					num = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_UNKNOWN");
					break;
				case Gender.Male:
					num = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_MALE");
					break;
				case Gender.Female:
					num = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_FEMALE");
					break;
				}
				if (num.HasValue)
				{
					androidJavaObject.Call<AndroidJavaObject>("setGender", new object[1] { num });
				}
			}
			if (request.TagForChildDirectedTreatment.HasValue)
			{
				androidJavaObject.Call<AndroidJavaObject>("tagForChildDirectedTreatment", new object[1] { request.TagForChildDirectedTreatment.GetValueOrDefault() });
			}
			androidJavaObject.Call<AndroidJavaObject>("setRequestAgent", new object[1] { "unity-3.1.3" });
			AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("android.os.Bundle");
			foreach (KeyValuePair<string, string> extra in request.Extras)
			{
				androidJavaObject3.Call("putString", extra.Key, extra.Value);
			}
			AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("com.google.android.gms.ads.mediation.admob.AdMobExtras", androidJavaObject3);
			androidJavaObject.Call<AndroidJavaObject>("addNetworkExtras", new object[1] { androidJavaObject4 });
			return androidJavaObject.Call<AndroidJavaObject>("build", new object[0]);
		}
	}
}
