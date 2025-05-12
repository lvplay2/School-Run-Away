using UnityEngine;

public class CanvasMiniMap : MonoBehaviour
{
	public Camera cam;

	public float w1;

	public float h1;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 vector = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f));
		Vector3 vector2 = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f));
		h1 = (vector - vector2).magnitude;
	}
}
