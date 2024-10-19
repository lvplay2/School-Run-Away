using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
	public Text textScore;

	public Text textTimeLeft;

	public GameObject panelStart;

	public GameObject panelFinish;

	public GameObject panelHeader;

	public GameObject mobileSimpleStickControl;

	public Text textFinishMessage;

	public Text textFinishScore;

	public static CanvasController instance;

	private void Awake()
	{
		instance = this;
		Text[] array = UnityEngine.Object.FindObjectsOfType<Text>();
		Text[] array2 = array;
		foreach (Text text in array2)
		{
			text.text = Localizer.LocalizedString(text.text);
		}
		GameObject gameObject = GameObject.Find("RoboAdBanner");
		if ((bool)gameObject)
		{
			gameObject.SendMessage("Hide");
			Debug.Log("RoboAdBanner Hide");
		}
	}

	public void ShowScore(float score)
	{
		string arg = Localizer.LocalizedString("Score:");
		textScore.text = string.Format("{0} {1:00000000}", arg, score);
	}

	public void ShowTimeLeft(float timeLeft)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
		string text = Localizer.LocalizedString("Time Left:");
		string text2 = string.Format("{0} {1:D1}:{2:D2}", text, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 100);
		textTimeLeft.text = text2;
	}

	public void HidePanelStart()
	{
		panelStart.SetActive(false);
	}

	public void ShowPanelFinish(float score, string message)
	{
		textFinishMessage.text = Localizer.LocalizedString(message);
		string arg = Localizer.LocalizedString("Score:");
		textFinishScore.text = string.Format("{0} {1:00000000}", arg, score);
		panelFinish.SetActive(true);
		panelHeader.SetActive(false);
		mobileSimpleStickControl.SetActive(false);
		Text[] array = UnityEngine.Object.FindObjectsOfType<Text>();
		Text[] array2 = array;
		foreach (Text text in array2)
		{
			text.text = Localizer.LocalizedString(text.text);
		}
		GameObject gameObject = GameObject.Find("RoboAdBanner");
		if ((bool)gameObject)
		{
			gameObject.SendMessage("Show");
		}
		GameObject gameObject2 = GameObject.Find("RoboInterstitial");
		if ((bool)gameObject2)
		{
			gameObject2.SendMessage("ShowLoadingSlider", GetComponentInChildren<AlertViewController>());
		}
	}
}
