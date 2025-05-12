using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
	public enum Clip
	{
		Select = 0,
		Menu = 1,
		Win = 2,
		Lose = 3,
		Play = 4
	}

	public AudioClip bgmSelect;

	public AudioClip bgmMenu;

	public AudioClip bgmWin;

	public AudioClip bgmLose;

	public AudioClip[] bgmPlay;

	private List<AudioClip> listClips;

	[HideInInspector]
	public AudioSource audioSource;

	public static BGMController instance;

	public float m_OriginalVolume = 0.1f;

	private float m_TargetVolume;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		instance = this;
		listClips = new List<AudioClip>(5);
		listClips.Add(bgmSelect);
		listClips.Add(bgmMenu);
		listClips.Add(bgmWin);
		listClips.Add(bgmLose);
		m_TargetVolume = m_OriginalVolume;
	}

	private void Update()
	{
		audioSource.volume = Mathf.Lerp(audioSource.volume, m_TargetVolume, Time.deltaTime * 2f);
	}

	public void PlayBGM(Clip clip)
	{
		audioSource.Stop();
		if (clip < Clip.Play)
		{
			audioSource.clip = listClips[(int)clip];
		}
		else
		{
			int num = Random.Range(0, bgmPlay.Length);
			audioSource.clip = bgmPlay[num];
		}
		audioSource.Play();
	}

	public void Mute(bool flg)
	{
		audioSource.mute = flg;
	}

	public void SetTargetVolumeOn(bool isOn)
	{
		m_TargetVolume = ((!isOn) ? 0f : m_OriginalVolume);
	}

	public void InitBGM(Clip clip)
	{
		bool flag = int.Parse((!PlayerPrefs.HasKey("str_BGM")) ? "0" : PlayerPrefs.GetString("str_BGM")) == 0;
		m_TargetVolume = ((!flag) ? 0f : m_OriginalVolume);
		audioSource.volume = m_TargetVolume;
		PlayBGM(clip);
	}
}
