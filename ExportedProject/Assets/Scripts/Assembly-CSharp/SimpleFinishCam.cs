using UnityEngine;
using UnityStandardAssets.Cameras;

public class SimpleFinishCam : PivotBasedCameraRig
{
	private bool m_LookFromFrontTrigger;

	private bool m_LookFromFront;

	public bool m_MoveWithinTime;

	public float m_TransformHeightOffset = -0.2f;

	public float m_PivotTiltTarget = -8f;

	public float m_WallClipTargetDistance = 1.5f;

	private Vector3 StartTransformPosition;

	private Quaternion StartTransformRotation;

	private Quaternion StartPivotRotation;

	private float StartWallClipDistance;

	private Vector3 EndTransformPosition;

	private Quaternion EndTransformRotation;

	private Quaternion EndPivotRotation;

	private float EndWallClipDistance;

	private float m_LookFromFrontTime = 2f;

	private float m_LookFromFrontElappsedTime;

	private float m_MoveSpeed = 12f;

	private float m_TurnSpeed = 1f;

	private bool transformPositionDone;

	private bool transformPositionAlmostDone;

	private bool transformRotationDone;

	private bool transformRotationAlmostDone;

	private bool pivotRotationDone;

	private bool wallClipDone;

	public bool m_Done;

	public bool m_AlmostDone;

	private SimpleAutoCam m_AutoCam;

	private SimpleFreeLookCam m_FreeCam;

	private SimpleLookatTarget m_CctvCam;

	private SimpleProtectCameraFromWallClip m_WallClip;

	private Vector3 m_TransformPositionOriginal;

	private void OnEnable()
	{
		m_WallClip = GetComponent<SimpleProtectCameraFromWallClip>();
		m_LookFromFrontTrigger = true;
		m_AutoCam = GetComponent<SimpleAutoCam>();
		m_FreeCam = GetComponent<SimpleFreeLookCam>();
		m_CctvCam = GetComponent<SimpleLookatTarget>();
		m_TransformPositionOriginal = base.transform.position;
	}

	private void OnDisable()
	{
		m_WallClip.m_OriginalDist = StartWallClipDistance;
		CamEnabled(true);
		if ((bool)m_CctvCam)
		{
			base.transform.position = m_TransformPositionOriginal;
		}
	}

	protected override void FollowTarget(float deltaTime)
	{
		if (deltaTime > 0f && !(m_Target == null))
		{
			LookFromFrontTrigger();
			LookFromFront(deltaTime);
		}
	}

	private void LookFromFrontTrigger()
	{
		if (m_LookFromFrontTrigger && !m_LookFromFront)
		{
			m_LookFromFront = true;
			StartTransformPosition = base.transform.position;
			StartTransformRotation = base.transform.rotation;
			StartPivotRotation = m_Pivot.localRotation;
			StartWallClipDistance = m_WallClip.m_OriginalDist;
			m_LookFromFrontElappsedTime = 0f;
		}
	}

	private void LookFromFront(float deltaTime)
	{
		if (!m_LookFromFront)
		{
			return;
		}
		CamEnabled(false);
		if (m_MoveWithinTime)
		{
			m_LookFromFrontElappsedTime += Time.deltaTime;
			float num = m_LookFromFrontElappsedTime / m_LookFromFrontTime;
			EndTransformPosition = m_Target.position + Vector3.up * m_TransformHeightOffset;
			EndTransformRotation = Quaternion.LookRotation(-m_Target.forward, Vector3.up);
			EndPivotRotation = Quaternion.Euler(m_PivotTiltTarget, 0f, 0f);
			EndWallClipDistance = m_WallClipTargetDistance;
			base.transform.position = Vector3.Lerp(StartTransformPosition, EndTransformPosition, num);
			base.transform.rotation = Quaternion.Slerp(StartTransformRotation, EndTransformRotation, num);
			m_Pivot.localRotation = Quaternion.Slerp(StartPivotRotation, EndPivotRotation, num);
			m_WallClip.m_OriginalDist = Mathf.Lerp(StartWallClipDistance, EndWallClipDistance, num);
			if (num >= 1f)
			{
				m_LookFromFrontTrigger = false;
				m_LookFromFront = false;
			}
		}
		if (!m_MoveWithinTime)
		{
			EndTransformPosition = m_Target.position + Vector3.up * m_TransformHeightOffset;
			EndTransformRotation = Quaternion.LookRotation(-m_Target.forward, Vector3.up);
			EndPivotRotation = Quaternion.Euler(m_PivotTiltTarget, 0f, 0f);
			EndWallClipDistance = m_WallClipTargetDistance;
			transformPositionDone = (EndTransformPosition - base.transform.position).magnitude < 1f;
			transformPositionAlmostDone = (EndTransformPosition - base.transform.position).magnitude < 2f;
			transformRotationDone = Quaternion.Angle(base.transform.rotation, EndTransformRotation) < 1f;
			transformRotationAlmostDone = Quaternion.Angle(base.transform.rotation, EndTransformRotation) < 30f;
			pivotRotationDone = Quaternion.Angle(m_Pivot.localRotation, EndPivotRotation) < 1f;
			wallClipDone = m_WallClip.m_OriginalDist - EndWallClipDistance < 0.1f;
			base.transform.position = Vector3.Lerp(base.transform.position, EndTransformPosition, m_MoveSpeed * deltaTime);
			if (transformPositionAlmostDone)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, EndTransformRotation, m_TurnSpeed * deltaTime);
				m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, EndPivotRotation, m_TurnSpeed * deltaTime);
				m_WallClip.m_OriginalDist = Mathf.Lerp(m_WallClip.m_OriginalDist, EndWallClipDistance, m_TurnSpeed * deltaTime);
			}
			m_Done = transformPositionDone & transformRotationDone & pivotRotationDone & wallClipDone;
			m_AlmostDone = transformPositionDone & transformRotationAlmostDone & pivotRotationDone & wallClipDone;
			if (m_Done)
			{
				m_LookFromFrontTrigger = false;
				m_LookFromFront = false;
			}
		}
	}

	private void CamEnabled(bool flg)
	{
		if (m_AutoCam != null)
		{
			m_AutoCam.enabled = flg;
		}
		if (m_FreeCam != null)
		{
			m_FreeCam.enabled = flg;
		}
		if (m_CctvCam != null)
		{
			m_CctvCam.enabled = flg;
		}
	}
}
