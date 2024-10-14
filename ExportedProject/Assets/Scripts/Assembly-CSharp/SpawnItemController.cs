using UnityEngine;

public class SpawnItemController : MonoBehaviour
{
	public GameObject powerItemPrefab;

	public GameObject barrierItemPrefab;

	public int maxCount = 20;

	private GameObject itemFolder;

	private SpawnCloud spawnCloud;

	public static SpawnItemController instance;

	private void Awake()
	{
		instance = this;
		itemFolder = GameObject.Find("ItemFolder");
		if (itemFolder == null)
		{
			itemFolder = new GameObject("ItemFolder");
		}
	}

	private void Start()
	{
		spawnCloud = Object.FindObjectOfType<SpawnCloud>();
	}

	private void SpawnItem(GameObject prefab)
	{
		Vector3 newCloudPosition = spawnCloud.GetNewCloudPosition();
		GameObject gameObject = (GameObject)Object.Instantiate(prefab, newCloudPosition, Quaternion.identity);
		gameObject.transform.SetParent(itemFolder.transform);
		gameObject.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
	}

	public void InitSpawn()
	{
		for (int i = 0; i < maxCount; i++)
		{
			if (Random.Range(0, 2) == 0)
			{
				SpawnItem(powerItemPrefab);
			}
			else
			{
				SpawnItem(barrierItemPrefab);
			}
		}
	}
}
