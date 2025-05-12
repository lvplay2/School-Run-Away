using UnityEngine;

public class FootSound : MonoBehaviour
{
	private AudioSource audioSource;

	private HunterLeg hunterLeg;

	private PlayerCharacter playerCharacter;

	public AudioClip clipFootstep;

	public AudioClip clipFootLanding;

	private bool previousOnGoundStatus;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		hunterLeg = GetComponent<HunterLeg>();
		playerCharacter = GetComponent<PlayerCharacter>();
		previousOnGoundStatus = true;
	}

	private void Update()
	{
		CheckPlaySoundFootLanding();
	}

	private void CheckPlaySoundFootLanding()
	{
		if (playerCharacter != null)
		{
			if (!previousOnGoundStatus && playerCharacter.onGround)
			{
				audioSource.PlayOneShot(clipFootLanding);
			}
			previousOnGoundStatus = playerCharacter.onGround;
		}
		if (hunterLeg != null)
		{
			if (!previousOnGoundStatus && hunterLeg.m_IsGrounded)
			{
				audioSource.PlayOneShot(clipFootLanding);
			}
			previousOnGoundStatus = hunterLeg.m_IsGrounded;
		}
	}

	public void PlaySoundFootStep()
	{
		audioSource.PlayOneShot(clipFootstep);
	}
}
