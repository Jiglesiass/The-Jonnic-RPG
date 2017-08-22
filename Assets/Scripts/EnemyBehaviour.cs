using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent), typeof(BoxCollider), typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
	private Player player;
	private NavMeshAgent agent;
	private Animator anim;

	private bool attacking;
	private bool chasing;
	private float range = 2f;

	private void Awake ()
	{
		player = FindObjectOfType<Player>();
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		if (chasing)
		{
			float distanceToPlayer = CheckDistanceToPlayer();
			
			if (distanceToPlayer <= range)
			{
				agent.isStopped = true;
				Attack();
			}
		}
	}

	private void Attack()
	{
		Debug.Log(name + "attacked");
	}

	private void OnTriggerStay(Collider other)
	{
		Player player = other.GetComponentInChildren<Player>();

		if (player && !attacking)
		{
			ChasePlayer();
			chasing = true;
		}
		else
		{
			chasing = false;
		}
	}

	private void ChasePlayer()
	{
		agent.destination = player.transform.position;
	}

	private float CheckDistanceToPlayer()
	{
		return (player.transform.position - transform.position).magnitude;
	}
}
