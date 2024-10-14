using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(HunterLeg))]
public class HunterAILeg : MonoBehaviour
{
	[HideInInspector]
	public Transform target;

	[HideInInspector]
	public bool jump;

	[HideInInspector]
	public UnityEngine.AI.NavMeshAgent agent { get; private set; }

	[HideInInspector]
	public HunterLeg character { get; private set; }

	private void Start()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		character = GetComponent<HunterLeg>();
		agent.updateRotation = false;
		agent.updatePosition = true;
		jump = false;
	}

	private void Update()
	{
		if (target != null)
		{
			if (agent.enabled && agent.isOnNavMesh)
			{
				agent.SetDestination(target.position);
			}
			character.Move(agent.desiredVelocity, false, jump);
			if (jump)
			{
				agent.enabled = false;
				jump = false;
			}
		}
		else
		{
			character.Move(Vector3.zero, false, false);
			agent.enabled = false;
		}
		if (character.m_IsGrounded)
		{
			agent.enabled = true;
		}
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}
