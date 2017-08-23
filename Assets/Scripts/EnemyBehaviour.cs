using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent), typeof(BoxCollider), typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
	private Player player;
	private NavMeshAgent agent;
	private Animator anim;

	private bool chase;
	private bool isAttacking;
	private float range = 2f;

	private void Awake ()
	{
		player = FindObjectOfType<Player>();
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (chase)
		{
			ChasePlayer();

			float distanceToPlayer = CheckDistanceToPlayer();
			
			if (distanceToPlayer <= range && !isAttacking)
			{
				Attack();
			}
		}
		else
		{
			anim.SetBool("isMoving", false);
		}
	}

	private void Attack()
	{
		var targetRot = Quaternion.LookRotation(player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5f * Time.deltaTime);
		isAttacking = true;
		agent.isStopped = true;
		anim.SetTrigger("attack");
	}

	public void ResumeMovement()
	{
		Debug.Log("Resume movement called");
		agent.isStopped = false;
		isAttacking = false;
	}

	private void ChasePlayer()
	{
		agent.destination = player.transform.position;
		anim.SetBool("isMoving", true);
	}

	private float CheckDistanceToPlayer()
	{
		return (player.transform.position - transform.position).magnitude;
	}

	public void SetChase(bool newValue)
	{
		chase = newValue;
	}
}
