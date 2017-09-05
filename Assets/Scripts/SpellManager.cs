using System.Collections.Generic;
using UnityEngine;

// TODO: Add T, 1, 2 and 3. 
//		 Make sure all SkillButton have they "key" value assigned in the inspector (maybe assign it by default through code just to make sure)
//		 Check it works
//		 Add damage logic
public class SpellManager : MonoBehaviour
{
	public static Dictionary<string, SkillButton> spellsPrefabs;
	public float globalCooldown = 1f;

	private Player player;
	private PlayerAnimator playerAnim;

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
		else
		{
			skillButton.Launch(player.transform);
			playerAnim.SetAnimatorTrigger(animatorTriggerName);
		}
	}
}
