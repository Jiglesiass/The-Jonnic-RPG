using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class SkillButton : MonoBehaviour
{
	public string key;
	public GameObject particlePrefab;
	public Vector3 offset;
	public SpellAttributes spellAtributes;

	private Player player;
	private bool inCD;
	private Transform particleHolder;

	private void Awake()
	{
		player = FindObjectOfType<Player>();
		particleHolder = GameObject.Find("Particles").transform;
		Assert.IsNotNull(particleHolder, "Particles GO not found. Please create it.");
	}

	public void Launch(Transform parent)
	{
		if (inCD)
		{
			Debug.Log(name + "is in cooldown");
			return;
		}
		if (player.Mana <= spellAtributes.manaCost)
		{
			Debug.Log("Not enough mana");
			return;
		}

		GameObject spell = Instantiate(particlePrefab, player.transform.position + offset, Quaternion.identity, particleHolder);

		Vector3 direction = new Vector3();
		RaycastHit hit;
		int layerMask = 1 << 8;
		float speed = spellAtributes.projectileSpeed;

		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask))
		{
			direction = (hit.point - player.transform.position).normalized;
		}
		if (speed > 0f)
		{
			spell.GetComponent<Rigidbody>().velocity = direction * speed;
		}

		player.ConsumeMana(spellAtributes.manaCost);
		StartCoroutine("Cooldown");
	}

	[Serializable]
	public class SpellAttributes
	{
		public float damage = 10f;
		public float manaCost = 10f;
		public float projectileSpeed = 1f;
		public float cooldown = 5f;
		[Range(1, 10)]
		public int level = 1;
	}

	private IEnumerator Cooldown()
	{
		inCD = true;
		yield return new WaitForSeconds(spellAtributes.cooldown);
		inCD = false;
	}
}


