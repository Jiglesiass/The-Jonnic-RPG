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
	private Transform skillLauncher;
	private PlayerAnimator playerAnim;

	private void Awake()
	{
		player = FindObjectOfType<Player>();
		playerAnim = FindObjectOfType<PlayerAnimator>();

		particleHolder = GameObject.Find("Particles").transform;
		Assert.IsNotNull(particleHolder, "Particles GO not found. Please create it.");

		skillLauncher = GameObject.Find("SkillLauncher").transform;
		Assert.IsNotNull(skillLauncher, "SkillLauncher GO not found. Please create it.");
	}

	public bool IsInCD()
	{
		return inCD;
	}

	public void Launch(Transform parent)
	{
		if (inCD)
		{
			Debug.Log(name + "is in cooldown");
			return;
		}

		playerAnim.StopAndTurnToMousePosition();
		StartCoroutine(playerAnim.StopAgent(0.3f));
		StartCoroutine(playerAnim.SwitchToAttackStance(0.3f));

		Quaternion rot = skillLauncher.rotation;
		GameObject spell = Instantiate(particlePrefab, skillLauncher.position + offset, rot, skillLauncher);

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


