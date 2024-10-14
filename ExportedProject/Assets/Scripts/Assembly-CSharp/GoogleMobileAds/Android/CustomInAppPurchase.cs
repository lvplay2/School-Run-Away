using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class CustomInAppPurchase : ICustomInAppPurchase
	{
		private AndroidJavaObject purchase;

		public string ProductId
		{
			get
			{
				return purchase.Call<string>("getProductId", new object[0]);
			}
		}

		public CustomInAppPurchase(AndroidJavaObject purchase)
		{
			this.purchase = purchase;
		}

		public void RecordResolution(PurchaseResolutionType resolution)
		{
			purchase.Call("recordResolution", (int)resolution);
		}
	}
}
