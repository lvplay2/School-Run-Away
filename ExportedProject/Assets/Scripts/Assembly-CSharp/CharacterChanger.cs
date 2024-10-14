using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
	public int characterID;

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		ChangeClothes(ChangeCharacter());
	}

	private void Update()
	{
	}

	private int ChangeCharacter()
	{
		int num = Random.Range(0, 3);
		int num2 = num;
		for (int i = 0; i < 3; i++)
		{
			if (i != num2)
			{
				Object.Destroy(base.transform.GetChild(i).gameObject);
			}
		}
		characterID = num2;
		return num2;
	}

	private void ChangeClothes(int targetID)
	{
		Transform child = base.transform.GetChild(targetID);
		int childCount = child.childCount;
		int num = Random.Range(0, childCount);
		for (int i = 0; i < childCount; i++)
		{
			if (i == num)
			{
				GameObject gameObject = child.GetChild(i).gameObject;
				gameObject.SetActive(true);
				Animator component = child.GetChild(i).GetComponent<Animator>();
				anim.avatar = component.avatar;
				Object.Destroy(component);
				HairChanger component2 = GetComponent<HairChanger>();
				if (component2 != null)
				{
					component2.ChangeHairColor(gameObject);
					component2.ChangeHairSize(gameObject);
				}
			}
			else
			{
				Object.Destroy(child.GetChild(i).gameObject);
			}
		}
	}
}
