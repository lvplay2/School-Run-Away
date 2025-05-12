using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public static SceneController instance;

	private AudioSource bgmAudioSource;

	private AudioSource seAudioSource;

	private float timeFadeOut;

	private void Awake()
	{
		instance = this;
		Handheld.StopActivityIndicator();
		AudioListener.volume = 1f;
	}

	public void Reload()
	{
		LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadScene(string sceneName, float timeFadeOut)
	{
		Handheld.StartActivityIndicator();
		bgmAudioSource = BGMController.instance.audioSource;
		seAudioSource = SoundController.instance.audioSource;
		this.timeFadeOut = Mathf.Clamp(timeFadeOut, 0.01f, 5f);
		Object.DontDestroyOnLoad(this);
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
		SceneManager.LoadScene(sceneName);
	}

	public void LoadScene(string sceneName)
	{
		LoadScene(sceneName, 2f);
	}

	private void SceneManager_sceneUnloaded(Scene arg0)
	{
		SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
		StartCoroutine(SceneUnloaded());
	}

	private IEnumerator SceneUnloaded()
	{
		float volume = bgmAudioSource.volume;
		float elappsedTime = 0f;
		while (elappsedTime < timeFadeOut)
		{
			elappsedTime += Time.deltaTime;
			float v = Mathf.Lerp(volume, 0f, elappsedTime / timeFadeOut);
			bgmAudioSource.volume = v;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitUntil(() => !seAudioSource.isPlaying);
		Object.Destroy(base.gameObject);
	}
}
