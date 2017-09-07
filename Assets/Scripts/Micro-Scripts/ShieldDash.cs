using UnityEngine;

public class ShieldDash : MonoBehaviour
{
	private ParticleSystem[] particles;

	private void Awake()
	{
		particles = GetComponentsInChildren<ParticleSystem>();
	}

	public void PlayParticle()
	{
		foreach (ParticleSystem particle in particles)
		{
			particle.Play();
		}
	}
}
