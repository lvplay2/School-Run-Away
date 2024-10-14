namespace GoogleMobileAds.Api
{
	public interface ICustomInAppPurchase
	{
		string ProductId { get; }

		void RecordResolution(PurchaseResolutionType resolution);
	}
}
