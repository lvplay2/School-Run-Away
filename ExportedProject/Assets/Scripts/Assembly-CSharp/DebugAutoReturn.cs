using UnityEngine;
using UnityEngine.UI;

public class DebugAutoReturn : MonoBehaviour
{
	public Toggle m_ToggleAutoReturn;

	public Toggle m_ToggleOldCameraRig;

	public GameObject m_SimpleCameraRig;

	public GameObject m_OldCameraRig;

	private SimpleAutoCam m_SimpleAutoCam;

	private void Start()
	{
		m_SimpleAutoCam = Object.FindObjectOfType<SimpleAutoCam>();
	}

	private void Update()
	{
	}

	public void ChangeAutoReturnValue()
	{
		bool isOn = m_ToggleAutoReturn.isOn;
		m_SimpleAutoCam.m_AutoReturn = isOn;
	}

	public void ChangeOldCameraRig()
	{
		bool isOn = m_ToggleOldCameraRig.isOn;
		m_SimpleCameraRig.SetActive(!isOn);
		m_OldCameraRig.SetActive(isOn);
	}
}
