using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class InAppPurchaseResult : IInAppPurchaseResult
	{
		private AndroidJavaObject result;

		public string ProductId
		{
			get
			{
				return result.Call<string>("getProductId", new object[0]);
			}
		}

		public bool IsSuccessful
		{
			get
			{
				AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.unity.ads.PluginUtils");
				return androidJavaObject.CallStatic<bool>("isResultSuccess", new object[1] { result });
			}
		}

		public bool IsVerified
		{
			get
			{
				return result.Call<bool>("isVerified", new object[0]);
			}
		}

		public InAppPurchaseResult(AndroidJavaObject result)
		{
			this.result = result;
		}

		public void FinishPurchase()
		{
			result.Call("finishPurchase");
		}
	}
}
