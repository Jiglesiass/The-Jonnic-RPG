using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

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
	private Image cooldownFill;

	private void Awake()
	{
		player = FindObjectOfType<Player>();
		playerAnim = FindObjectOfType<PlayerAnimator>();

		particleHolder = GameObject.Find("Particles").transform;
		Assert.IsNotNull(particleHolder, "Particles GO not found. Please create it.");

		skillLauncher = GameObject.Find("SkillLauncher").transform;
		Assert.IsNotNull(skillLauncher, "SkillLauncher GO not found. Please create it.");

		cooldownFill = transform.GetChild(0).GetComponent<Image>();
		Assert.IsNotNull(cooldownFill, "cooldownFill Image not found.");
	}

	private void Start()
	{
		cooldownFill.fillAmount = 0f;
	}

	public bool IsInCD()
	{
		return inCD;
	}

	public void Launch(Transform parent)
	{
		playerAnim.StopAndTurnToMousePosition();
		StartCoroutine(playerAnim.StopAgent(spellAtributes.castingTime));
		StartCoroutine(playerAnim.SwitchToAttackStance(spellAtributes.castingTime));
		StartCoroutine("Cooldown");

		StartCoroutine(LaunchParticle(spellAtributes.particleDelay));
	}

	[Serializable]
	public class SpellAttributes
	{
		public float damage = 10f;
		public float projectileSpeed = 0f;
		public float cooldown = 2f;
		public float castingTime = 0.3f;
		public float particleDelay = 0f;
		[Range(1, 10)]
		public int level = 1;
	}

	private IEnumerator Cooldown()
	{
		inCD = true;
		cooldownFill.fillAmount = 1f;
		cooldownFill.DOFillAmount(0f, spellAtributes.cooldown);
		yield return new WaitForSeconds(spellAtributes.cooldown);
		inCD = false;
	}

	public IEnumerator GlobalCooldown(float time)
	{
		if (!inCD)
		{
			inCD = true;
			cooldownFill.fillAmount = 1f;
			cooldownFill.DOFillAmount(0f, time);
			yield return new WaitForSeconds(time);
			inCD = false;
		}
	}

	private IEnumerator LaunchParticle(float delay)
	{
		yield return new WaitForSeconds(delay);

		Quaternion rot = skillLauncher.rotation;
		GameObject spell = Instantiate(particlePrefab, skillLauncher.position, rot, skillLauncher);
		spell.transform.localPosition += offset;
	}
}


