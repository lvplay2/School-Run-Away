using UnityEngine;

public class PlayerStaminaController : MonoBehaviour
{
	public AudioClip clipGetSpeed;

	public float timeSpeedRemain;

	private GameObject player;

	private AudioSource audioSource;

	private PlayerCharacter playerCharacter;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerCharacter = player.GetComponent<PlayerCharacter>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		CheckSpeed();
	}

	private void GetSpeedItem()
	{
		GameController.instance.AddScore(10000f);
		timeSpeedRemain += 5f;
		timeSpeedRemain = Mathf.Clamp(timeSpeedRemain, 0f, 5f);
		audioSource.PlayOneShot(clipGetSpeed);
	}

	private void CheckSpeed()
	{
		timeSpeedRemain -= Time.deltaTime;
		if (timeSpeedRemain > 0f)
		{
			playerCharacter.animSpeedMultiplier = 1.5f;
			return;
		}
		timeSpeedRemain = 0f;
		playerCharacter.animSpeedMultiplier = 1f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PowerItem"))
		{
			GetSpeedItem();
			Object.Destroy(other.gameObject);
		}
	}
}
