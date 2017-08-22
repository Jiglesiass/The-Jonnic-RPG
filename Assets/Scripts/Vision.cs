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
		Player player = other.GetComponentInChildren<Player>();

		if (player)
		{
			behaviour.SetChase(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Player player = other.GetComponentInChildren<Player>();

		if (player)
		{
			behaviour.SetChase(false);
		}
	}
}
