using UnityEngine;
using UnityStandardAssets.Cameras;

public class SimpleLookatTarget : PivotBasedCameraRig
{
	[SerializeField]
	private Vector2 m_RotationRange;

	[SerializeField]
	private float m_TurnSpeed = 1f;

	private Quaternion m_TransformTargetRotation;

	private Quaternion m_PivotTargetRotation;

	protected override void FollowTarget(float deltaTime)
	{
		Vector3 normalized = (m_Target.position - base.transform.position).normalized;
		normalized = ((!(normalized == Vector3.zero)) ? normalized : base.transform.forward);
		m_TransformTargetRotation = Quaternion.LookRotation(normalized, Vector3.up);
		float y = m_TransformTargetRotation.eulerAngles.y;
		y = ((!(y > 180f)) ? y : (y - 360f));
		y = Mathf.Clamp(y, (0f - m_RotationRange.y) * 0.5f, m_RotationRange.y * 0.5f);
		m_TransformTargetRotation = Quaternion.Euler(0f, y, 0f);
		Vector3 normalized2 = (m_Target.position - m_Cam.position).normalized;
		normalized2 = ((!(normalized2 == Vector3.zero)) ? normalized2 : m_Cam.forward);
		m_PivotTargetRotation = Quaternion.LookRotation(normalized2, Vector3.up);
		float x = m_PivotTargetRotation.eulerAngles.x;
		x = ((!(x > 180f)) ? x : (x - 360f));
		x = Mathf.Clamp(x, (0f - m_RotationRange.x) * 0.5f, m_RotationRange.x * 0.5f);
		m_PivotTargetRotation = Quaternion.Euler(x, 0f, 0f);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, m_TransformTargetRotation, m_TurnSpeed * deltaTime);
		m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRotation, m_TurnSpeed * deltaTime);
	}
}
