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

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				agent.destination = hit.point;
			}
		}
	}
}
