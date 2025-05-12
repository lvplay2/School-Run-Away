using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoboLoading : MonoBehaviour
{
	public bool isTestMode;

	private float elappsedTimeFromBeginning;

	private float elappsedTimeAfterAdLoaded;

	private bool lastSplashScreenStatus;

	private bool isDisappearedSlpashScreen;

	private bool isAdLoaded;

	private bool isAdShown;

	private AlertViewController avc;

	private void Start()
	{
		Text[] array = UnityEngine.Object.FindObjectsOfType<Text>();
		Text[] array2 = array;
		foreach (Text text in array2)
		{
			text.text = Localizer.LocalizedString(text.text);
		}
		string text2 = Application.systemLanguage.ToString();
		if (!text2.Equals("Japanese"))
		{
			StartCoroutine(DoMenu());
			return;
		}
		Handheld.StartActivityIndicator();
		avc = UnityEngine.Object.FindObjectOfType<AlertViewController>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		elappsedTimeFromBeginning += Time.deltaTime;
		if (!isAdShown && elappsedTimeFromBeginning > 12f)
		{
			StartCoroutine(DoMenu());
		}
		LoadingAndroid();
	}

	private void LoadingAndroid()
	{
		if (!isAdLoaded)
		{
			return;
		}
		elappsedTimeAfterAdLoaded += Time.deltaTime;
		if (!isAdShown)
		{
			avc.sliderLoading.value = elappsedTimeAfterAdLoaded;
		}
	}

	private void LoadingIOS()
	{
		if (lastSplashScreenStatus && !Application.isShowingSplashScreen)
		{
			isDisappearedSlpashScreen = true;
		}
		lastSplashScreenStatus = Application.isShowingSplashScreen;
		if (!isAdLoaded || !isDisappearedSlpashScreen)
		{
			return;
		}
		elappsedTimeAfterAdLoaded += Time.deltaTime;
		if (!isAdShown)
		{
			avc.sliderLoading.value = elappsedTimeAfterAdLoaded;
		}
	}

	private void InterstitialLoaded(object sender, EventArgs e)
	{
		isAdLoaded = true;
	}

	private void InterstitialClosed(object sender, EventArgs e)
	{
		StartCoroutine(DoMenu());
	}

	private void InterstitialFailedToLoad(object sender, EventArgs e)
	{
		StartCoroutine(DoMenu());
	}

	private IEnumerator DoMenu()
	{
		yield return new WaitForSeconds(0.1f);
		Handheld.StartActivityIndicator();
		SceneController.instance.LoadScene("Main", 0f);
	}
}
