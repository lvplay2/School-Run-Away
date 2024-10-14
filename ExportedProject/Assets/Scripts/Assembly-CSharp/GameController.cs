using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
	public AudioClip clipStart;

	public AudioClip clipFinish;

	public AudioClip clipReload;

	public AudioClip clipBackToMenu;

	public bool isPlaying;

	private float score;

	private float timeLeft = 90f;

	private GameObject player;

	public static GameController instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Joystick joystick = Object.FindObjectOfType<Joystick>();
		if ((bool)joystick)
		{
			joystick.enabled = false;
			joystick.enabled = true;
		}
		ViewDidLoad();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			DoMenu();
		}
		if (Input.GetKey(KeyCode.R))
		{
			DoRestart();
		}
		ReduceTime();
	}

	public void AddScore(float amount)
	{
		if (isPlaying)
		{
			score += amount;
			CanvasController.instance.ShowScore(score);
		}
	}

	private void ReduceTime()
	{
		if (isPlaying)
		{
			timeLeft -= Time.deltaTime;
			timeLeft = Mathf.Clamp(timeLeft, 0f, timeLeft);
			CanvasController.instance.ShowTimeLeft(timeLeft);
			if (timeLeft <= 0f)
			{
				Win();
			}
		}
	}

	private void Win()
	{
		Debug.Log("Player Win");
		Animator playerAnimator = GetPlayerAnimator();
		playerAnimator.SetBool("Win", true);
		BGMController.instance.PlayBGM(BGMController.Clip.Win);
		CanvasController.instance.ShowPanelFinish(score, "You Win!!");
		GameOver();
	}

	public void Lose()
	{
		Debug.Log("Player Lose");
		Animator playerAnimator = GetPlayerAnimator();
		playerAnimator.SetBool("Died", true);
		BGMController.instance.PlayBGM(BGMController.Clip.Lose);
		CanvasController.instance.ShowPanelFinish(score, "You Lose");
		GameOver();
	}

	private void GameOver()
	{
		isPlaying = false;
		SoundController.instance.PlayOneShot(clipFinish);
		SimpleAutoCam simpleAutoCam = Object.FindObjectOfType<SimpleAutoCam>();
		if (simpleAutoCam != null)
		{
			simpleAutoCam.m_LookFromFront = true;
		}
	}

	private Animator GetPlayerAnimator()
	{
		Animator component = player.GetComponent<Animator>();
		component.SetInteger("CharacterID", player.GetComponent<CharacterChanger>().characterID);
		return component;
	}

	private IEnumerator DoStart()
	{
		SimpleAutoCam autoCam = Object.FindObjectOfType<SimpleAutoCam>();
		if (autoCam != null)
		{
			autoCam.transform.position = player.transform.position;
			autoCam.transform.rotation = player.transform.rotation;
			autoCam.transform.Rotate(Vector3.up, 180f);
		}
		yield return new WaitForSeconds(2.5f);
		CanvasController.instance.HidePanelStart();
		isPlaying = true;
		SoundController.instance.PlayOneShot(clipStart);
	}

	private void ViewDidLoad()
	{
		isPlaying = false;
		BGMController.instance.PlayBGM(BGMController.Clip.Play);
		SpawnHunterController.instance.InitSpawn();
		SpawnItemController.instance.InitSpawn();
		CanvasController.instance.ShowScore(score);
		CanvasController.instance.ShowTimeLeft(timeLeft);
		StartCoroutine(DoStart());
	}

	public void DoMenu()
	{
		SoundController.instance.Play(clipBackToMenu);
		SceneController.instance.LoadScene("Main", 1f);
	}

	public void DoRestart()
	{
		SoundController.instance.Play(clipReload);
		SceneController.instance.Reload();
	}
}
