using UnityEngine;
using UnityEngine.Rendering;

public class ShadowOff : MonoBehaviour
{
	private void Start()
	{
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			renderer.shadowCastingMode = ShadowCastingMode.Off;
		}
	}
}
