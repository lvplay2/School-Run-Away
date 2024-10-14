using UnityEngine;

public class ItemFall : MonoBehaviour
{
	private void OnCollisionEnter()
	{
		Rigidbody component = GetComponent<Rigidbody>();
		Object.Destroy(component);
	}
}
