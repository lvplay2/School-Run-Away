using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	internal interface INativeExpressAdClient
	{
		event EventHandler<EventArgs> OnAdLoaded;

		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		event EventHandler<EventArgs> OnAdOpening;

		event EventHandler<EventArgs> OnAdClosed;

		event EventHandler<EventArgs> OnAdLeavingApplication;

		void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position);

		void LoadAd(AdRequest request);

		void ShowNativeExpressAdView();

		void HideNativeExpressAdView();

		void DestroyNativeExpressAdView();
	}
}
