using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.CrossPlatformInput;

public class SimpleAutoCam : PivotBasedCameraRig
{
	public enum TurnDotType
	{
		Standard = 0,
		Forward = 1,
		Side = 2
	}

	public float m_MoveSpeed = 12f;

	public float m_TurnSpeed = 1f;

	public TurnDotType m_TurnDotType = TurnDotType.Forward;

	[Header("Tilt")]
	public bool m_UseTilt;

	public float m_TiltUpMax = 5f;

	public float m_TiltDownMax = 40f;

	public float m_TiltSpeed = 4f;

	public float m_TiltVelocityMultiplier = 2f;

	[Header("LookForward")]
	public string m_LookForwardButtonName = "Fire2";

	public float m_LookForwardSpeed = 2f;

	public bool m_LookForwardWhenStart = true;

	private bool m_LookForwardTrigger;

	private bool m_LookForward;

	private Vector3 m_LookForwardTarget;

	[Header("LookFromFront")]
	public bool m_LookFromFront;

	[Header("Others")]
	public bool m_AutoReturn;

	public float m_MoveBackSpeed = 36f;

	private Quaternion m_TransformTargetRotation;

	private Quaternion m_PivotTargetRotation;

	private float m_TransformMoveAmount;

	private float m_TransformTurnAmount;

	private float m_PivotTurnAmount;

	protected override void FollowTarget(float deltaTime)
	{
		if (deltaTime > 0f && !(m_Target == null))
		{
			m_TransformTargetRotation = Quaternion.LookRotation(m_Target.forward, Vector3.up);
			m_TransformTurnAmount = m_TurnSpeed;
			m_PivotTurnAmount = m_TiltSpeed;
			TurnUpDown();
			TurnLeftRight();
			LookForward();
			LookFromFront();
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, m_TransformTargetRotation, m_TransformTurnAmount * deltaTime);
			m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRotation, m_PivotTurnAmount * deltaTime);
			m_TransformMoveAmount = m_MoveSpeed;
			AdjustMoveSpeed();
			base.transform.position = Vector3.Lerp(base.transform.position, m_Target.position, m_TransformMoveAmount * deltaTime);
		}
	}

	private void TurnLeftRight()
	{
		float num = Mathf.Clamp01(Vector3.Scale(targetRigidbody.velocity, new Vector3(1f, 0f, 1f)).magnitude);
		float num2 = Vector3.Dot(m_Target.forward, base.transform.forward);
		if (m_TurnDotType == TurnDotType.Forward)
		{
			num2 = (num2 + 1f) * 0.5f;
		}
		else if (m_TurnDotType == TurnDotType.Side)
		{
			num2 = 1f - Mathf.Abs(num2);
		}
		float num3 = num2 * num;
		if (num < 0.5f && m_AutoReturn)
		{
			num3 = Mathf.Max(num3, 0.1f);
		}
		m_TransformTurnAmount = num3 * m_TurnSpeed;
	}

	private void TurnUpDown()
	{
		if (m_UseTilt)
		{
			float value = (0f - targetRigidbody.velocity.y) * m_TiltVelocityMultiplier;
			value = Mathf.Clamp(value, 0f - m_TiltUpMax, m_TiltDownMax);
			m_PivotTargetRotation = Quaternion.Euler(value, 0f, 0f);
		}
		else
		{
			m_PivotTargetRotation = m_Pivot.localRotation;
		}
	}

	private void LookForward()
	{
		m_LookForwardTrigger = CrossPlatformInputManager.GetButtonDown(m_LookForwardButtonName);
		if (m_LookForwardWhenStart)
		{
			m_LookForwardTrigger = true;
			m_LookForwardWhenStart = false;
		}
		if (m_LookForwardTrigger && !m_LookForward)
		{
			m_LookForwardTarget = m_Target.forward;
			m_LookForward = true;
			m_LookForwardTrigger = false;
		}
		if (m_LookForward)
		{
			m_TransformTargetRotation = Quaternion.LookRotation(m_LookForwardTarget, Vector3.up);
			m_TransformTurnAmount = m_LookForwardSpeed;
			if (Vector3.Dot(m_LookForwardTarget, base.transform.forward) > 0.99f)
			{
				m_LookForward = false;
			}
		}
	}

	private void LookFromFront()
	{
		if (m_LookFromFront)
		{
			m_TransformTargetRotation = Quaternion.LookRotation(-m_Target.forward, Vector3.up);
			m_TransformTurnAmount = m_TurnSpeed;
		}
	}

	private void AdjustMoveSpeed()
	{
		float num = Vector3.Dot(m_Target.forward, base.transform.forward);
		num = (num + 1f) * 0.5f;
		num = 1f - num;
		m_TransformMoveAmount = Mathf.Max(num * m_MoveBackSpeed, m_MoveSpeed);
	}
}
