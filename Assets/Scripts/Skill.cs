using System;
using UnityEngine;

[Serializable]
public class SpellAttributes
{
	public float damage = 10f;
	public float manaCost = 10f;
	public float projectileSpeed = 1f;
	public float cooldown = 1f;
	[Range(1, 10)]
	public int level = 1;
}

public class Skill : MonoBehaviour
{
	public GameObject particlePrefab;
	public Vector3 offset;
	public SpellAttributes spellAtributes;

	public void Launch(Transform parent)
	{
		GameObject spell = Instantiate(particlePrefab, offset, Quaternion.identity, parent);

		float speed = spellAtributes.projectileSpeed;
		if (speed > 0f)
		{
			spell.GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
		}
	}
}


