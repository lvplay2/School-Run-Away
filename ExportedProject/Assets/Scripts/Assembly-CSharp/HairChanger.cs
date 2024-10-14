using UnityEngine;

public class HairChanger : MonoBehaviour
{
	public bool changeHairColor = true;

	public bool changeHairSize = true;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ChangeHairColor(GameObject targetCharacter)
	{
		if (!changeHairColor)
		{
			return;
		}
		SkinnedMeshRenderer componentInChildren = targetCharacter.GetComponentInChildren<SkinnedMeshRenderer>();
		Material[] materials = componentInChildren.materials;
		Material material = null;
		Material[] array = materials;
		foreach (Material material2 in array)
		{
			if (material2.name.Contains("hair"))
			{
				material = material2;
			}
		}
		if (material != null)
		{
			Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			material.color = color;
		}
	}

	public void ChangeHairSize(GameObject targetCharacter)
	{
		if (changeHairSize)
		{
			SkinnedMeshRenderer componentInChildren = targetCharacter.GetComponentInChildren<SkinnedMeshRenderer>();
			Transform[] bones = componentInChildren.bones;
			Transform transform = FindBoneWithName("BackHair1", bones);
			if (transform != null)
			{
				float num = Random.Range(0f, 1.5f);
				transform.localScale = Vector3.one * num;
			}
			float num2 = Random.Range(0.5f, 1f);
			Transform transform2 = FindBoneWithName("RightHair1", bones);
			if (transform2 != null)
			{
				transform2.localScale = Vector3.one * num2;
			}
			Transform transform3 = FindBoneWithName("LeftHair1", bones);
			if (transform3 != null)
			{
				transform3.localScale = Vector3.one * num2;
			}
		}
	}

	private Transform FindBoneWithName(string name, Transform[] bones)
	{
		Transform result = null;
		foreach (Transform transform in bones)
		{
			if (transform.name.Equals(name))
			{
				result = transform;
			}
		}
		return result;
	}
}
