using UnityEngine;

public class HunterInAir : MonoBehaviour
{
	private HunterAILeg aiLeg;

	private Rigidbody rbody;

	private float timeInAir;

	private void Start()
	{
		aiLeg = GetComponent<HunterAILeg>();
		rbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		MoveForwardInAir();
		CheckStackInAir();
	}

	private void MoveForwardInAir()
	{
		float z = 2f;
		float num = 3f;
		if (!aiLeg.character.m_IsGrounded)
		{
			rbody.AddRelativeForce(0f, 0f, z);
			Vector3 velocity = rbody.velocity;
			float y = velocity.y;
			velocity.y = 0f;
			if (velocity.magnitude > num)
			{
				velocity = velocity.normalized * num;
				velocity.y = y;
				rbody.velocity = velocity;
			}
		}
	}

	private void CheckStackInAir()
	{
		if (aiLeg.character.m_IsGrounded)
		{
			timeInAir = 0f;
			return;
		}
		timeInAir += Time.deltaTime;
		if (timeInAir > 5f)
		{
			Debug.Log("Stack in air");
			aiLeg.character.m_GroundCheckDistance = 5f;
		}
	}
}
