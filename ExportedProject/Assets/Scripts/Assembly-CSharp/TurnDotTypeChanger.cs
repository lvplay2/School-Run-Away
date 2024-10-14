using UnityEngine;
using UnityEngine.UI;

public class TurnDotTypeChanger : MonoBehaviour
{
	private SimpleAutoCam m_SimpleAutoCam;

	private Dropdown m_Dropdown;

	private void Start()
	{
		m_Dropdown = GetComponent<Dropdown>();
		m_SimpleAutoCam = Object.FindObjectOfType<SimpleAutoCam>();
		if (m_SimpleAutoCam != null)
		{
			m_Dropdown.value = (int)m_SimpleAutoCam.m_TurnDotType;
		}
	}

	private void Update()
	{
	}

	public void TurnDotTypeChanged()
	{
		m_SimpleAutoCam.m_TurnDotType = (SimpleAutoCam.TurnDotType)m_Dropdown.value;
	}
}
