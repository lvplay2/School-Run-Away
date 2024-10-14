using System.Collections;
using UnityEngine;

public class SpawnHunterController : MonoBehaviour
{
	public static SpawnHunterController instance;

	public GameObject hunterPrefab;

	public bool m_NoHunterForTest;

	private int maxHunterCount;

	private float spawnInterval = 5f;

	private GameObject player;

	private GameObject hunterFolder;

	private SpawnCloud spawnCloud;

	private void Awake()
	{
		instance = this;
		hunterFolder = GameObject.Find("HunterFolder");
		if (hunterFolder == null)
		{
			hunterFolder = new GameObject("HunterFolder");
		}
		int num = int.Parse((!PlayerPrefs.HasKey("str_level")) ? "0" : PlayerPrefs.GetString("str_level"));
		maxHunterCount = num * 5 + 10;
		if (m_NoHunterForTest)
		{
			maxHunterCount = 0;
		}
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		spawnCloud = Object.FindObjectOfType<SpawnCloud>();
		StartCoroutine(SpawnAndRemoveHunter());
	}

	public bool SpawnHunter(Transform spawnTransform)
	{
		if (Physics.CheckSphere(spawnTransform.position + Vector3.up * 1.5f, 0.5f))
		{
			return false;
		}
		Vector3 position = spawnTransform.position;
		Quaternion rotation = spawnTransform.rotation;
		GameObject gameObject = (GameObject)Object.Instantiate(hunterPrefab, position, rotation);
		gameObject.transform.SetParent(hunterFolder.transform);
		return true;
	}

	public void RemoveHunter()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Hunter");
		float num = 0f;
		GameObject gameObject = null;
		GameObject[] array2 = array;
		foreach (GameObject gameObject2 in array2)
		{
			float sqrMagnitude = (player.transform.position - gameObject2.transform.position).sqrMagnitude;
			if (sqrMagnitude > num)
			{
				num = sqrMagnitude;
				gameObject = gameObject2;
			}
		}
		if (gameObject != null)
		{
			Object.Destroy(gameObject);
		}
	}

	private int CountHunter()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Hunter");
		return array.Length;
	}

	private void SpawnHunterFromNearestSpawn()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("HunterSpawn");
		float num = float.MaxValue;
		GameObject gameObject = null;
		GameObject[] array2 = array;
		foreach (GameObject gameObject2 in array2)
		{
			Vector3 position = player.transform.position;
			Vector3 position2 = gameObject2.transform.position;
			position.y = 0f;
			position2.y = 0f;
			float sqrMagnitude = (position - position2).sqrMagnitude;
			if (sqrMagnitude > 25f && sqrMagnitude < num)
			{
				num = sqrMagnitude;
				gameObject = gameObject2;
			}
		}
		if (gameObject != null)
		{
			SpawnHunter(gameObject.transform);
		}
	}

	private IEnumerator SpawnAndRemoveHunter()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);
			if (GameController.instance.isPlaying)
			{
				SpawnHunterFromNearestSpawn();
				if (CountHunter() > maxHunterCount)
				{
					RemoveHunter();
				}
			}
		}
	}

	public void InitSpawn()
	{
		for (int i = 0; i < maxHunterCount; i++)
		{
			Vector3 newGroundPositionAwayFromPlayer = spawnCloud.GetNewGroundPositionAwayFromPlayer(20f);
			base.transform.position = newGroundPositionAwayFromPlayer;
			SpawnHunter(base.transform);
		}
	}
}
