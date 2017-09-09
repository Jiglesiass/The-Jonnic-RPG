using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
	private NavMeshAgent agent;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
	{
		if (Input.GetMouseButton(1))
		{
			RaycastHit hit;
			int layerMask = 1 << 8;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask) && PlayerAnimator.GetPlayerState() != PlayerState.attacking)
			{
				agent.destination = hit.point;
			}
		}
	}

	public void ResetAgentDestination()
	{
		agent.SetDestination(transform.position);
	}
}
