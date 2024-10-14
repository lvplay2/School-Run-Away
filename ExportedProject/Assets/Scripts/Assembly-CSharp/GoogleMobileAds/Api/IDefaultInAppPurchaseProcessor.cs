namespace GoogleMobileAds.Api
{
	public interface IDefaultInAppPurchaseProcessor
	{
		string AndroidPublicKey { get; }

		void ProcessCompletedInAppPurchase(IInAppPurchaseResult result);

		bool IsValidPurchase(string sku);
	}
}
