using System;
using System.Runtime.CompilerServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class RewardBasedVideoAdClient : AndroidJavaProxy, IRewardBasedVideoAdClient
	{
		private AndroidJavaObject androidRewardBasedVideo;

		[method: MethodImpl(32)]
		public event EventHandler<EventArgs> OnAdLoaded = delegate
		{
		};

		[method: MethodImpl(32)]
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad = delegate
		{
		};

		[method: MethodImpl(32)]
		public event EventHandler<EventArgs> OnAdOpening = delegate
		{
		};

		[method: MethodImpl(32)]
		public event EventHandler<EventArgs> OnAdStarted = delegate
		{
		};

		[method: MethodImpl(32)]
		public event EventHandler<EventArgs> OnAdClosed = delegate
		{
		};

		[method: MethodImpl(32)]
		public event EventHandler<Reward> OnAdRewarded = delegate
		{
		};

		[method: MethodImpl(32)]
		public event EventHandler<EventArgs> OnAdLeavingApplication = delegate
		{
		};

		public RewardBasedVideoAdClient()
			: base("com.google.unity.ads.UnityRewardBasedVideoAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			androidRewardBasedVideo = new AndroidJavaObject("com.google.unity.ads.RewardBasedVideo", @static, this);
		}

		public void CreateRewardBasedVideoAd()
		{
			androidRewardBasedVideo.Call("create");
		}

		public void LoadAd(AdRequest request, string adUnitId)
		{
			androidRewardBasedVideo.Call("loadAd", Utils.GetAdRequestJavaObject(request), adUnitId);
		}

		public bool IsLoaded()
		{
			return androidRewardBasedVideo.Call<bool>("isLoaded", new object[0]);
		}

		public void ShowRewardBasedVideoAd()
		{
			androidRewardBasedVideo.Call("show");
		}

		public void DestroyRewardBasedVideoAd()
		{
			androidRewardBasedVideo.Call("destroy");
		}

		private void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		private void onAdFailedToLoad(string errorReason)
		{
			if (this.OnAdFailedToLoad != null)
			{
				AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
				adFailedToLoadEventArgs.Message = errorReason;
				AdFailedToLoadEventArgs e = adFailedToLoadEventArgs;
				this.OnAdFailedToLoad(this, e);
			}
		}

		private void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		private void onAdStarted()
		{
			if (this.OnAdStarted != null)
			{
				this.OnAdStarted(this, EventArgs.Empty);
			}
		}

		private void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		private void onAdRewarded(string type, float amount)
		{
			if (this.OnAdRewarded != null)
			{
				Reward reward = new Reward();
				reward.Type = type;
				reward.Amount = amount;
				Reward e = reward;
				this.OnAdRewarded(this, e);
			}
		}

		private void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}
	}
}
