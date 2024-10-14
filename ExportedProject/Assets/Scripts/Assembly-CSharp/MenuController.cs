using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public GameObject tgLevel;

	public AudioClip clipToggle;

	public AudioClip clipStart;

	private List<Toggle> listLevel;

	private void Start()
	{
		BGMController.instance.PlayBGM(BGMController.Clip.Menu);
		listLevel = new List<Toggle>(5);
		Text[] array = Object.FindObjectsOfType<Text>();
		Text[] array2 = array;
		foreach (Text text in array2)
		{
			text.text = Localizer.LocalizedString(text.text);
		}
		SetToggles("str_level", listLevel, tgLevel);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void SetToggles(string keyString, List<Toggle> list, GameObject tg)
	{
		int index = int.Parse((!PlayerPrefs.HasKey(keyString)) ? "0" : PlayerPrefs.GetString(keyString));
		list.AddRange(tg.transform.GetComponentsInChildren<Toggle>());
		list[index].isOn = true;
	}

	private void SaveToggles(string keyString, List<Toggle> list, GameObject tg)
	{
		List<Toggle> list2 = new List<Toggle>(5);
		list2.AddRange(tg.GetComponent<ToggleGroup>().ActiveToggles());
		int num = list.IndexOf(list2[0]);
		PlayerPrefs.SetString(keyString, string.Format("{0}", num));
	}

	private void DoSave()
	{
		SaveToggles("str_level", listLevel, tgLevel);
		PlayerPrefs.Save();
	}

	public void DoStart()
	{
		DoSave();
		SoundController.instance.Play(clipStart);
		SceneController.instance.LoadScene("Field");
	}

	public void SoundToggleClick()
	{
		SoundController.instance.PlayOneShot(clipToggle);
	}
}
