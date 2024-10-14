using GoogleMobileAds.Android;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
	internal class GoogleMobileAdsClientFactory
	{
		internal static IBannerClient BuildBannerClient()
		{
			return new BannerClient();
		}

		internal static IInterstitialClient BuildInterstitialClient()
		{
			return new InterstitialClient();
		}

		internal static IRewardBasedVideoAdClient BuildRewardBasedVideoAdClient()
		{
			return new RewardBasedVideoAdClient();
		}

		internal static IAdLoaderClient BuildAdLoaderClient(AdLoader adLoader)
		{
			return new AdLoaderClient(adLoader);
		}

		internal static INativeExpressAdClient BuildNativeExpressAdClient()
		{
			return new NativeExpressAdClient();
		}
	}
}
