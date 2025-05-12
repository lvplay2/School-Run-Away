using UnityEngine;

public class HunterSpawnTrigger : MonoBehaviour
{
	public Transform[] hunterSpawns;

	private void SpawnHunter()
	{
		if (hunterSpawns.Length > 0)
		{
			int num = Random.Range(0, hunterSpawns.Length);
			if (SpawnHunterController.instance.SpawnHunter(hunterSpawns[num]))
			{
				SpawnHunterController.instance.RemoveHunter();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SpawnHunter();
		}
	}
}
