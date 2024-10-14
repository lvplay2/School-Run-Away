using UnityEngine;

public class Hunter : MonoBehaviour
{
	private HunterAILeg aiLeg;

	private GameObject objPlayer;

	private GameObject target;

	private SpawnCloud spawnCloud;

	private GameObject hunterTargetFolder;

	private void Awake()
	{
		hunterTargetFolder = GameObject.Find("HunterTargetFolder");
		if (hunterTargetFolder == null)
		{
			hunterTargetFolder = new GameObject("HunterTargetFolder");
		}
	}

	private void Start()
	{
		spawnCloud = GameObject.Find("SpawnCloud").GetComponent<SpawnCloud>();
		aiLeg = GetComponent<HunterAILeg>();
		objPlayer = GameObject.FindGameObjectWithTag("Player");
		UnityEngine.AI.NavMeshAgent component = GetComponent<UnityEngine.AI.NavMeshAgent>();
		component.speed = Random.Range(0.8f, 1f);
		component.avoidancePriority = Random.Range(1, 100);
		target = new GameObject("HunterTarget");
		target.transform.SetParent(hunterTargetFolder.transform);
		SetTargetToNear();
	}

	private void Update()
	{
		if (!GameController.instance.isPlaying)
		{
			aiLeg.SetTarget(null);
		}
		else
		{
			CheckAgent();
		}
	}

	private void SetTargetToNear()
	{
		Vector3 newGroundPositionAroundObject = spawnCloud.GetNewGroundPositionAroundObject(base.gameObject, 40f);
		target.transform.position = newGroundPositionAroundObject;
		aiLeg.SetTarget(target.transform);
	}

	private void SetTargetToPlayer()
	{
		target.transform.position = objPlayer.transform.position;
		aiLeg.SetTarget(target.transform);
	}

	private void CheckAgent()
	{
		if (objPlayer == null)
		{
			objPlayer = GameObject.FindGameObjectWithTag("Player");
		}
		float sqrMagnitude = (base.transform.position - objPlayer.transform.position).sqrMagnitude;
		if (sqrMagnitude < 400f)
		{
			SetTargetToPlayer();
		}
		else if (aiLeg.target == null)
		{
			SetTargetToNear();
		}
		else if ((target.transform.position - base.transform.position).sqrMagnitude < 100f)
		{
			SetTargetToNear();
		}
		else if (aiLeg.agent != null && !aiLeg.agent.hasPath)
		{
			SetTargetToNear();
		}
	}

	private void OnDestroy()
	{
		Object.Destroy(target);
	}
}
