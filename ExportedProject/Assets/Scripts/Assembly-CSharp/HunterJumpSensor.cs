using UnityEngine;

public class HunterJumpSensor : MonoBehaviour
{
	private float defaultJumpPower = 20f;

	private float elappsedTime;

	private float jumpWaitTime = 5f;

	private bool canJump;

	private HunterAILeg aiLeg;

	private GameObject objPlayer;

	private void Start()
	{
		aiLeg = GetComponent<HunterAILeg>();
		objPlayer = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (!canJump)
		{
			elappsedTime += Time.deltaTime;
			if (elappsedTime > jumpWaitTime)
			{
				canJump = true;
			}
		}
		CheckJump();
	}

	private void CheckJump()
	{
		float num = 2f;
		Vector3 vector = new Vector3(0f, 0.5f, 0.5f);
		Vector3 vector2 = base.transform.position + base.transform.TransformVector(vector);
		Vector3 vector3 = base.transform.forward * num + Vector3.up * 0.5f;
		Debug.DrawRay(vector2, vector3, Color.red);
		RaycastHit hitInfo;
		Physics.Raycast(vector2, vector3, out hitInfo, num);
		if (hitInfo.collider != null)
		{
			Jump(hitInfo.collider);
		}
	}

	private void Jump(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("Hunter") || other.CompareTag("Stairs") || !canJump)
		{
			return;
		}
		float num = objPlayer.transform.position.y - base.transform.position.y;
		if (!(num < 0.1f))
		{
			if (aiLeg == null)
			{
				aiLeg = GetComponentInParent<HunterAILeg>();
			}
			aiLeg.character.m_JumpPower = defaultJumpPower * Random.Range(0.3f, 1f);
			aiLeg.jump = true;
		}
	}
}
