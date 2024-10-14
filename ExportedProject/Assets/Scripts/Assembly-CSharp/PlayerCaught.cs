using System.Collections;
using UnityEngine;

public class PlayerCaught : MonoBehaviour
{
	public bool isNonCaughtTime;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void CheckCollision(Collision other)
	{
		if (GameController.instance.isPlaying && !isNonCaughtTime && other.collider.CompareTag("Hunter"))
		{
			PlayerBarrierController component = GetComponent<PlayerBarrierController>();
			if (component != null && component.countBarrier > 0)
			{
				component.RemoveBarrierItem();
				StartCoroutine(InNonCaughtTime());
			}
			else
			{
				GameController.instance.Lose();
			}
		}
	}

	private IEnumerator InNonCaughtTime()
	{
		isNonCaughtTime = true;
		yield return new WaitForSeconds(2f);
		isNonCaughtTime = false;
	}

	private void OnCollisionStay(Collision other)
	{
		CheckCollision(other);
	}

	private void OnCollisionEnter(Collision other)
	{
		CheckCollision(other);
	}
}
