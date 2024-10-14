using UnityEngine;
using UnityEngine.UI;

public class AlertViewController : MonoBehaviour
{
	public Text textMessage;

	public Button buttonOk;

	public Slider sliderLoading;

	private GameObject callBackGameObject;

	private string callBackMethod;

	public void ShowAlertView(string msg)
	{
		ShowAlertView(msg, null, null);
	}

	public void ShowAlertView(string msg, GameObject obj, string cbm)
	{
		if (textMessage != null)
		{
			textMessage.text = Localizer.LocalizedString(msg);
		}
		callBackGameObject = obj;
		callBackMethod = cbm;
	}

	public void ShowAlertView(string msg, GameObject obj, string cbm, bool withInterstitial)
	{
		ShowAlertView(msg, obj, cbm);
		if (withInterstitial)
		{
			GameObject gameObject = GameObject.Find("RoboInterstitial");
			if (gameObject != null)
			{
				gameObject.SendMessage("ShowLoadingSlider", this);
			}
		}
	}

	public void DoButtonOkPressed()
	{
		if (callBackGameObject != null)
		{
			callBackGameObject.SendMessage(callBackMethod);
		}
		Object.Destroy(base.gameObject);
	}
}
