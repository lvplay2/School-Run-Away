using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class SimpleJoystick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
{
	public enum AxisOption
	{
		Both = 0,
		OnlyHorizontal = 1,
		OnlyVertical = 2
	}

	public int MovementRange = 100;

	public AxisOption axesToUse;

	public string horizontalAxisName = "Horizontal";

	public string verticalAxisName = "Vertical";

	private Vector3 m_StartPos;

	private bool m_UseX;

	private bool m_UseY;

	private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

	private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

	public bool m_UseGrid;

	private float m_GridCount = 2f;

	public bool m_UseCircle = true;

	private void InitJoystick()
	{
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		Vector3[] array = new Vector3[4];
		component.GetWorldCorners(array);
		float num = array[1].y - array[0].y;
		MovementRange = (int)(num / 2f);
	}

	private void OnEnable()
	{
		CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);
		CrossPlatformInputManager.UnRegisterVirtualAxis(verticalAxisName);
		CreateVirtualAxes();
	}

	private void Start()
	{
		m_StartPos = base.transform.position;
		InitJoystick();
	}

	private void UpdateVirtualAxes(Vector3 value)
	{
		Vector3 vector = m_StartPos - value;
		vector.y = 0f - vector.y;
		vector /= (float)MovementRange;
		if (m_UseX)
		{
			m_HorizontalVirtualAxis.Update(0f - vector.x);
		}
		if (m_UseY)
		{
			m_VerticalVirtualAxis.Update(vector.y);
		}
	}

	private void CreateVirtualAxes()
	{
		m_UseX = axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal;
		m_UseY = axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical;
		if (m_UseX)
		{
			m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
		}
		if (m_UseY)
		{
			m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
		}
	}

	public void OnDrag(PointerEventData data)
	{
		Vector3 zero = Vector3.zero;
		if (m_UseX)
		{
			int value = (int)(data.position.x - m_StartPos.x);
			value = Mathf.Clamp(value, -MovementRange, MovementRange);
			zero.x = value;
		}
		if (m_UseY)
		{
			int value2 = (int)(data.position.y - m_StartPos.y);
			value2 = Mathf.Clamp(value2, -MovementRange, MovementRange);
			zero.y = value2;
		}
		if (m_UseGrid && !m_UseCircle)
		{
			float num = (float)MovementRange / m_GridCount;
			zero.x = (int)(Mathf.Round(zero.x / num) * num);
			zero.y = (int)(Mathf.Round(zero.y / num) * num);
		}
		base.transform.position = new Vector3(m_StartPos.x + zero.x, m_StartPos.y + zero.y, m_StartPos.z + zero.z);
		UpdateVirtualAxes(base.transform.position);
		if (m_UseCircle)
		{
			zero = Vector3.ClampMagnitude(zero, MovementRange);
			if (m_UseGrid)
			{
				float num2 = (float)MovementRange / m_GridCount;
				float num3 = Mathf.Round(zero.magnitude / num2) * num2;
				float num4 = 45f / m_GridCount;
				float num5 = Vector3.Angle(Vector3.right, zero) * Mathf.Sign(zero.y);
				float num6 = Mathf.Round(num5 / num4) * num4;
				float f = num6 * ((float)Math.PI / 180f);
				zero = Vector3.Scale(Vector3.one, new Vector3(Mathf.Cos(f), Mathf.Sin(f), 0f)) * num3;
			}
			base.transform.position = new Vector3(m_StartPos.x + zero.x, m_StartPos.y + zero.y, m_StartPos.z + zero.z);
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		base.transform.position = m_StartPos;
		UpdateVirtualAxes(m_StartPos);
	}

	public void OnPointerDown(PointerEventData data)
	{
	}

	private void OnDisable()
	{
		if (m_UseX)
		{
			m_HorizontalVirtualAxis.Remove();
		}
		if (m_UseY)
		{
			m_VerticalVirtualAxis.Remove();
		}
	}
}
