using UnityEngine;

public class MiniMapArrow : MonoBehaviour
{
	private Transform m_Player;

	private MeshRenderer m_MeshRenderer;

	private float m_CloseThreshold = 45f;

	private void Start()
	{
		m_Player = GameObject.FindGameObjectWithTag("Player").transform;
		m_MeshRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		if (!(m_Player == null))
		{
			float magnitude = (m_Player.position - base.transform.position).magnitude;
			if (magnitude < m_CloseThreshold)
			{
				m_MeshRenderer.enabled = true;
			}
			else
			{
				m_MeshRenderer.enabled = false;
			}
		}
	}
}
