using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class DefaultInAppPurchaseListener : AndroidJavaProxy
	{
		public IDefaultInAppPurchaseProcessor inAppPurchaseProcessor;

		internal DefaultInAppPurchaseListener(IDefaultInAppPurchaseProcessor inAppPurchaseProcessor)
			: base("com.google.android.gms.ads.purchase.PlayStorePurchaseListener")
		{
			this.inAppPurchaseProcessor = inAppPurchaseProcessor;
		}

		private bool isValidPurchase(string sku)
		{
			return inAppPurchaseProcessor.IsValidPurchase(sku);
		}

		private void onInAppPurchaseFinished(AndroidJavaObject result)
		{
			InAppPurchaseResult inAppPurchaseResult = new InAppPurchaseResult(result);
			if (inAppPurchaseResult.IsSuccessful && inAppPurchaseResult.IsVerified)
			{
				inAppPurchaseProcessor.ProcessCompletedInAppPurchase(inAppPurchaseResult);
			}
		}
	}
}
