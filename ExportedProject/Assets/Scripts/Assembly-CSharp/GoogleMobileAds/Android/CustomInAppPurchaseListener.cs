using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class CustomInAppPurchaseListener : AndroidJavaProxy
	{
		public ICustomInAppPurchaseProcessor inAppPurchaseProcessor;

		internal CustomInAppPurchaseListener(ICustomInAppPurchaseProcessor inAppPurchaseProcessor)
			: base("com.google.android.gms.ads.purchase.InAppPurchaseListener")
		{
			this.inAppPurchaseProcessor = inAppPurchaseProcessor;
		}

		private void onInAppPurchaseRequested(AndroidJavaObject result)
		{
			CustomInAppPurchase purchase = new CustomInAppPurchase(result);
			inAppPurchaseProcessor.ProcessInAppPurchase(purchase);
		}
	}
}
