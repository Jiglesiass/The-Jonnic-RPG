using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillParticlePrefabs : ScriptableObject 
{

	public List<SkillParticles> skillParticlesList;

	[Serializable]
	public class SkillParticles
	{
		public GameObject q;
		public GameObject e;
		public GameObject w;
		public GameObject r;
	}

	public SkillParticles GetParticles(Weapon weapon)
	{
		if (weapon == Weapon.SwordAndShield)
		{
			return skillParticlesList[0];
		}
		if (weapon == Weapon.Greatsword)
		{
			return skillParticlesList[1];
		}
		if (weapon == Weapon.Spear)
		{
			return skillParticlesList[2];
		}
		if (weapon == Weapon.FistWeapons)
		{
			return skillParticlesList[3];
		}

		return null;
	}
}
