using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
	public GameObject shieldActionBar;
	public GameObject spearActionBar;
	public GameObject greatswordActionBar;
	public GameObject fistActionBar;
	public RectTransform canvas;

	public float globalCooldown = 1f;

	private Dictionary<string, SkillButton> SpellButtons = new Dictionary<string, SkillButton>();
	private const string actionBarTag = "ActionBar";

	private GameObject currentActionBar;
	private Player player;
	private PlayerAnimator playerAnim;
	private PlayerMovement playerMov;

	private void Awake()
	{
		AddParticlesToDictionary();

		player = FindObjectOfType<Player>();
		playerAnim = FindObjectOfType<PlayerAnimator>();
		currentActionBar = GameObject.FindGameObjectWithTag(actionBarTag);
	}

	private void Update()
	{
		if (PlayerAnimator.GetPlayerState() == PlayerState.battleStance)
		{
			HandleInput();
		}
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			CastSpell("Q", "q");
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			CastSpell("W", "w");
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			CastSpell("E", "e");
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			CastSpell("R", "r");
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwapActionBar (Weapon.Spear);
			AddParticlesToDictionary();
			//playerAnim.SwapWeapon(Weapon.Spear);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwapActionBar (Weapon.Greatsword);
			AddParticlesToDictionary();
			//playerAnim.SwapWeapon(Weapon.Greatsword);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwapActionBar (Weapon.FistWeapons);
			AddParticlesToDictionary();
			//playerAnim.SwapWeapon(Weapon.FistWeapons);
		}
	}

	private void CastSpell(string keyLetter, string animatorTriggerName)
	{
		SkillButton skillButton;
		if (!SpellButtons.TryGetValue(keyLetter, out skillButton))
		{
			Debug.LogError(keyLetter + " SkillButton not found in dictionary");
		}
		else if (skillButton.IsInCD())
		{
			Debug.Log("Wait for cooldown!");
		}
		else
		{
			skillButton.Launch(player.transform);
			playerAnim.SetAnimatorTrigger(animatorTriggerName);

			foreach (var skill in SpellButtons)
			{
				StartCoroutine(skill.Value.GlobalCooldown(globalCooldown));
			}
		}
	}

	private void SwapActionBar(Weapon weapon)
	{
		if (weapon == Weapon.SwordAndShield)
		{
			InstantiateNewActionBar(shieldActionBar);
		}
		else if (weapon == Weapon.Greatsword)
		{
			InstantiateNewActionBar(greatswordActionBar);
		}
		else if (weapon == Weapon.Spear)
		{
			InstantiateNewActionBar(spearActionBar);
		}
		else if (weapon == Weapon.FistWeapons)
		{
			InstantiateNewActionBar(fistActionBar);
		}
	}

	private void AddParticlesToDictionary()
	{
		SpellButtons.Clear();

		foreach (SkillButton spellButton in FindObjectsOfType<SkillButton>())
		{
			SpellButtons.Add(spellButton.key, spellButton);
		}
	}

	private void InstantiateNewActionBar(GameObject actionBar)
	{
		Destroy(currentActionBar);
		GameObject newActionBar = Instantiate(actionBar, canvas);
		currentActionBar = newActionBar;
	}
}
