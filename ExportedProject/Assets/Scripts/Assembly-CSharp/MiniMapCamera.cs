using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
	public Transform m_RadarFrame;

	private Transform m_Player;

	private void Start()
	{
		m_Player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		if (!(m_Player == null))
		{
			base.transform.position = m_Player.position + Vector3.up * 100f;
			base.transform.rotation = Quaternion.LookRotation(Vector3.down, m_Player.forward);
			m_RadarFrame.transform.position = new Vector3(m_Player.position.x, -10f, m_Player.position.z);
		}
	}
}
