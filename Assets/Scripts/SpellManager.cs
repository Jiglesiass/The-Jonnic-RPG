using System.Collections.Generic;
using UnityEngine;

// TODO: Add T, 1, 2 and 3. ( T = Dodge // 1, 2 and 3 = Special Weapons )
//		 Add damage logic
public class SpellManager : MonoBehaviour
{
	public static Dictionary<string, SkillButton> spellsPrefabs;
	public float globalCooldown = 1f;

	private Player player;
	private PlayerAnimator playerAnim;
	private PlayerMovement playerMov;

	private void Awake()
	{
		spellsPrefabs = new Dictionary<string, SkillButton>();
		SkillButton[] skillButtons = FindObjectsOfType<SkillButton>();

		foreach (SkillButton spellButton in skillButtons)
		{
			spellsPrefabs.Add(spellButton.key, spellButton);
		}

		player = FindObjectOfType<Player>();
		playerAnim = FindObjectOfType<PlayerAnimator>();
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
	}

	private void CastSpell(string keyLetter, string animatorTriggerName)
	{
		SkillButton skillButton;
		if (!spellsPrefabs.TryGetValue(keyLetter, out skillButton))
		{
			Debug.LogError(keyLetter + " SkillButton not found in dictionary");
		}
		else if (player.Mana < skillButton.spellAtributes.manaCost)
		{
			Debug.Log("Not enough mana");
		}
		else if (skillButton.IsInCD())
		{
			Debug.Log("Wait for cooldown!");
		}
		else
		{
			skillButton.Launch(player.transform);
			playerAnim.SetAnimatorTrigger(animatorTriggerName);
		}
	}
}
