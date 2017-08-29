using System.Collections.Generic;
using UnityEngine;

// TODO: Add T, 1, 2 and 3. 
//		 Make sure all SkillButton have they "key" value assigned in the inspector (maybe assign it by default through code just to make sure)
//		 Check it works
public class SpellManager : MonoBehaviour
{
	public static Dictionary<string, SkillButton> spellsPrefabs;
	public float globalCooldown = 1f;

	private Transform player;

	private void Awake()
	{
		SkillButton[] skillButtons = FindObjectsOfType<SkillButton>();

		foreach (SkillButton spell in skillButtons)
		{
			spellsPrefabs.Add(spell.key, spell);
		}

		player = FindObjectOfType<Player>().transform;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			SkillButton skillButton;
			if (!spellsPrefabs.TryGetValue("Q", out skillButton))
			{
				Debug.LogError("Q SkillButton not found in dictionary");
			}
			else
			{
				skillButton.Launch(player);
			}
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			SkillButton skillButton;
			if (!spellsPrefabs.TryGetValue("W", out skillButton))
			{
				Debug.LogError("W SkillButton not found in dictionary");
			}
			else
			{
				skillButton.Launch(player);
			}
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			SkillButton skillButton;
			if (!spellsPrefabs.TryGetValue("E", out skillButton))
			{
				Debug.LogError("E SkillButton not found in dictionary");
			}
			else
			{
				skillButton.Launch(player);
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			SkillButton skillButton;
			if (!spellsPrefabs.TryGetValue("R", out skillButton))
			{
				Debug.LogError("R SkillButton not found in dictionary");
			}
			else
			{
				skillButton.Launch(player);
			}
		}
	}
}
