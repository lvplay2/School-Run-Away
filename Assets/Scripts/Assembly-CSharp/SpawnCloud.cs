using System.Collections.Generic;
using UnityEngine;

public class SpawnCloud : MonoBehaviour
{
	public int sizeStepWide = 1;

	public float collisionCheckSize = 4f;

	public bool showWaypoint;

	public GameObject waypointPrefab;

	private List<Vector3> listWaypoint;

	private void Start()
	{
		listWaypoint = new List<Vector3>(10000);
		MakeWaypoint();
	}

	private void MakeWaypoint()
	{
		int num = (int)base.transform.localScale.x / 2;
		int num2 = (int)base.transform.localScale.z / 2;
		for (int i = -num2; i < num2; i += sizeStepWide)
		{
			for (int j = -num; j < num; j += sizeStepWide)
			{
				Vector3 targetPosition = new Vector3(j, 0f, i) + base.transform.position;
				targetPosition = GetOnRoadPosition(targetPosition);
				if (targetPosition != Vector3.zero)
				{
					listWaypoint.Add(targetPosition);
					if (showWaypoint)
					{
						Object.Instantiate(waypointPrefab, targetPosition, Quaternion.identity);
					}
				}
			}
		}
		Debug.Log("Waypoint Count:" + listWaypoint.Count);
	}

	private Vector3 GetOnRoadPosition(Vector3 targetPosition)
	{
		Vector3 result = Vector3.zero;
		int num = 0;
		RaycastHit hitInfo;
		Physics.Raycast(targetPosition, -Vector3.up, out hitInfo, base.transform.position.y * 1.5f);
		if (hitInfo.collider != null)
		{
			if (num == 0)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(waypointPrefab, hitInfo.point, Quaternion.identity);
				gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
				UnityEngine.AI.NavMeshAgent component = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
				if (component.isOnNavMesh)
				{
					Vector3 point = hitInfo.point;
					Vector3 position = component.transform.position;
					point.y = position.y;
					if ((point - position).sqrMagnitude < 1f)
					{
						result = component.transform.position;
					}
				}
				Object.Destroy(gameObject);
			}
			else if (!CheckCollision(hitInfo.point))
			{
				result = hitInfo.point;
			}
		}
		return result;
	}

	private bool CheckCollision(Vector3 targetPosition)
	{
		return Physics.CheckSphere(targetPosition + Vector3.up * (collisionCheckSize + 1f), collisionCheckSize);
	}

	public Vector3 GetNewCloudPosition()
	{
		float x = Random.Range(-1f, 1f) * base.transform.localScale.x / 2f;
		float z = Random.Range(-1f, 1f) * base.transform.localScale.z / 2f;
		return new Vector3(x, 0f, z) + base.transform.position;
	}

	public Vector3 GetNewGroundPosition()
	{
		int index = Random.Range(0, listWaypoint.Count);
		return listWaypoint[index];
	}

	public Vector3 GetNewGroundPositionAroundObject(GameObject obj, float distanceThreshold)
	{
		List<Vector3> list = new List<Vector3>(10000);
		float num = distanceThreshold * distanceThreshold;
		for (int i = 0; i < listWaypoint.Count; i++)
		{
			float sqrMagnitude = (listWaypoint[i] - obj.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				list.Add(listWaypoint[i]);
			}
		}
		if (list.Count == 0)
		{
			Debug.Log("SpawnClowd:No target around object");
			int index = Random.Range(0, listWaypoint.Count);
			return listWaypoint[index];
		}
		int index2 = Random.Range(0, list.Count);
		return list[index2];
	}

	public Vector3 GetNewGroundPositionAwayFromPlayer(float distanceThreshold)
	{
		List<Vector3> list = new List<Vector3>(10000);
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		for (int i = 0; i < listWaypoint.Count; i++)
		{
			float magnitude = (listWaypoint[i] - gameObject.transform.position).magnitude;
			if (magnitude > distanceThreshold && !CheckCollision(listWaypoint[i]))
			{
				list.Add(listWaypoint[i]);
			}
		}
		if (list.Count == 0)
		{
			Debug.Log("SpawnClowd:No target away from player");
			int index = Random.Range(0, listWaypoint.Count);
			return listWaypoint[index];
		}
		int index2 = Random.Range(0, list.Count);
		return list[index2];
	}
}
