using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrierController : MonoBehaviour
{
	public int countBarrier;

	private int countBarrierMax = 3;

	public GameObject barrierPlatePrefab;

	public AudioClip clipGetBarrier;

	public AudioClip clipRemoveBarrier;

	private GameObject barrierPlate;

	private List<GameObject> listBarrierItems;

	private GameObject player;

	private AudioSource audioSource;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		audioSource = GetComponent<AudioSource>();
		InitBarrier();
	}

	private void Update()
	{
		RotateBarrierPlate();
	}

	private void InitBarrier()
	{
		barrierPlate = (GameObject)Object.Instantiate(barrierPlatePrefab, player.transform.position, player.transform.rotation);
		listBarrierItems = new List<GameObject>(countBarrierMax);
		for (int i = 0; i < barrierPlate.transform.childCount; i++)
		{
			listBarrierItems.Add(barrierPlate.transform.GetChild(i).gameObject);
		}
		ActivateBarrierItem();
	}

	private void RotateBarrierPlate()
	{
		barrierPlate.transform.position = player.transform.position + Vector3.up;
		barrierPlate.transform.Rotate(Vector3.up, Time.deltaTime * 180f);
	}

	private void GetBarrierItem()
	{
		GameController.instance.AddScore(10000f);
		countBarrier++;
		audioSource.PlayOneShot(clipGetBarrier);
		ActivateBarrierItem();
	}

	public void RemoveBarrierItem()
	{
		countBarrier--;
		audioSource.PlayOneShot(clipRemoveBarrier);
		ActivateBarrierItem();
		StartCoroutine(BlinkPlayer());
	}

	private IEnumerator BlinkPlayer()
	{
		SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		int length = 25;
		float waitTime = 1f / (float)length;
		for (int i = 0; i < length; i++)
		{
			renderer.enabled = false;
			barrierPlate.SetActive(false);
			yield return new WaitForSeconds(waitTime);
			renderer.enabled = true;
			barrierPlate.SetActive(true);
			yield return new WaitForSeconds(waitTime);
		}
	}

	private void ActivateBarrierItem()
	{
		countBarrier = Mathf.Clamp(countBarrier, 0, countBarrierMax);
		for (int i = 0; i < countBarrierMax; i++)
		{
			GameObject gameObject = listBarrierItems[i];
			if (i < countBarrier)
			{
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("BarrierItem"))
		{
			GetBarrierItem();
			Object.Destroy(other.gameObject);
		}
	}
}
