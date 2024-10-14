using UnityEngine;

public class FinishCamStarter : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.F))
		{
			return;
		}
		SimpleFinishCam simpleFinishCam = Object.FindObjectOfType<SimpleFinishCam>();
		if (simpleFinishCam.isActiveAndEnabled)
		{
			SimpleAutoCam simpleAutoCam = Object.FindObjectOfType<SimpleAutoCam>();
			if ((bool)simpleAutoCam)
			{
				simpleAutoCam.enabled = true;
			}
			SimpleFreeLookCam simpleFreeLookCam = Object.FindObjectOfType<SimpleFreeLookCam>();
			if (simpleFreeLookCam != null)
			{
				simpleFreeLookCam.enabled = true;
			}
			SimpleLookatTarget simpleLookatTarget = Object.FindObjectOfType<SimpleLookatTarget>();
			if (simpleLookatTarget != null)
			{
				simpleLookatTarget.enabled = true;
			}
			simpleFinishCam.enabled = false;
		}
		else
		{
			simpleFinishCam.enabled = true;
		}
	}
}
