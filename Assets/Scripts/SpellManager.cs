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

	private Transform player;

	private void Awake()
	{
		spellsPrefabs = new Dictionary<string, SkillButton>();
		SkillButton[] skillButtons = FindObjectsOfType<SkillButton>();

		foreach (SkillButton spellButton in skillButtons)
		{
			spellsPrefabs.Add(spellButton.key, spellButton);
		}

		player = FindObjectOfType<Player>().transform;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log("Input received");
			SkillButton skillButton;
			if (!spellsPrefabs.TryGetValue("Q", out skillButton))
			{
				Debug.LogError("Q SkillButton not found in dictionary");
			}
			else
			{
				Debug.Log(skillButton + "Launched");
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
