using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
	public enum Status
	{
		OnGround = 0,
		OnBuilding = 1,
		InAir = 2
	}

	public float timeOnGround;

	public float timeOnBuilding;

	public float timeInAir;

	public Status status;

	private PlayerCharacter character;

	private Rigidbody rbody;

	private void Start()
	{
		character = GetComponent<PlayerCharacter>();
		rbody = GetComponent<Rigidbody>();
		timeOnGround = 0f;
		timeOnBuilding = 0f;
		timeInAir = 0f;
	}

	private void Update()
	{
		if (GameController.instance.isPlaying)
		{
			UpdateStatistics();
			AddScore();
		}
	}

	private void UpdateStatistics()
	{
		bool flag = false;
		RaycastHit hitInfo;
		Physics.Raycast(base.transform.position + Vector3.up * 0.1f, Vector3.down, out hitInfo, 0.11f);
		if (hitInfo.collider != null && hitInfo.collider.CompareTag("Ground"))
		{
			flag = true;
		}
		if (character.onGround)
		{
			if (flag)
			{
				timeOnGround += Time.deltaTime;
				status = Status.OnGround;
			}
			else
			{
				timeOnBuilding += Time.deltaTime;
				status = Status.OnBuilding;
			}
		}
		else
		{
			timeInAir += Time.deltaTime;
			status = Status.InAir;
		}
	}

	private void AddScore()
	{
		float[] array = new float[3] { 1000f, 300f, 10f };
		if (rbody.velocity.sqrMagnitude > 0.1f)
		{
			GameController.instance.AddScore(Time.deltaTime * array[(int)status]);
		}
		else
		{
			GameController.instance.AddScore(Time.deltaTime * 2f);
		}
	}
}
