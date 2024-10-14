using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
	public AudioClip m_ClipClickUI1;

	public AudioClip m_ClipStart;

	[HideInInspector]
	public AudioSource audioSource;

	public static SoundController instance;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		instance = this;
	}

	public void PlayOneShot(AudioClip clip)
	{
		audioSource.PlayOneShot(clip);
	}

	public void Play(AudioClip clip)
	{
		audioSource.Stop();
		audioSource.clip = clip;
		audioSource.Play();
	}

	public void PlayClickUI1()
	{
		audioSource.PlayOneShot(m_ClipClickUI1);
	}

	public void PlayStart()
	{
		audioSource.PlayOneShot(m_ClipStart);
	}

	public void InitToggleClickSound(Transform toggleRoot)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener(delegate
		{
			instance.PlayClickUI1();
		});
		Toggle[] componentsInChildren = toggleRoot.GetComponentsInChildren<Toggle>();
		foreach (Toggle toggle in componentsInChildren)
		{
			EventTrigger eventTrigger = toggle.gameObject.AddComponent<EventTrigger>();
			eventTrigger.triggers.Add(entry);
		}
	}
}
