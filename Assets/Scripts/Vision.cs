using UnityEngine;

public class Vision : MonoBehaviour 
{
	private EnemyBehaviour behaviour;

	private void Awake()
	{
		behaviour = GetComponentInParent<EnemyBehaviour>();
	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerAnimator player = other.GetComponentInChildren<PlayerAnimator>();

		if (player)
		{
			behaviour.SetChase(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		PlayerAnimator player = other.GetComponentInChildren<PlayerAnimator>();

		if (player)
		{
			behaviour.SetChase(false);
		}
	}
}
