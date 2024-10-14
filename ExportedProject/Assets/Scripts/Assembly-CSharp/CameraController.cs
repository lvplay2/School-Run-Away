using UnityEngine;

public class CameraController : MonoBehaviour
{
	public AudioClip clipClick;

	private int cameraID;

	private ProtectCameraFromWallClip pcfwc;

	private float originalDistance;

	private void Start()
	{
		pcfwc = Object.FindObjectOfType<ProtectCameraFromWallClip>();
		InitCameras();
	}

	private void InitCameras()
	{
		cameraID = int.Parse((!PlayerPrefs.HasKey("str_cameraID")) ? "0" : PlayerPrefs.GetString("str_cameraID"));
		Debug.Log("cameraID:" + cameraID);
		ActivateCamera();
	}

	private void ActivateCamera()
	{
		pcfwc.SetCameraDistance((float)cameraID + 1f);
	}

	public void DoChangeCamera()
	{
		SoundController.instance.PlayOneShot(clipClick);
		cameraID++;
		if (cameraID >= 2)
		{
			cameraID = 0;
		}
		PlayerPrefs.SetString("str_cameraID", string.Format("{0}", cameraID));
		ActivateCamera();
	}
}
