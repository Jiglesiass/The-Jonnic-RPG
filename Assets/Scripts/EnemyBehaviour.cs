using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof (NavMeshAgent), typeof(BoxCollider), typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
	private PlayerAnimator playerAnimator;
	private NavMeshAgent agent;
	private Animator anim;
	private Player player;

	[SerializeField]
	private float damage = 25f;

	private bool chase;
	private bool isAttacking;
	private float range = 2f;

	private void Awake ()
	{
		agent = GetComponent<NavMeshAgent>();
		GetReferences();
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
		transform.DOLookAt(playerAnimator.transform.position, 0.2f);
		isAttacking = true;
		agent.isStopped = true;
		anim.SetTrigger("attack");
	}

	public void Hit()
	{
		player.TakeDamage(damage);
	}

	public void ResumeMovement()
	{
		agent.isStopped = false;
		isAttacking = false;
	}

	private void ChasePlayer()
	{
		agent.destination = playerAnimator.transform.position;
		anim.SetBool("isMoving", true);
	}

	private float CheckDistanceToPlayer()
	{
		return (playerAnimator.transform.position - transform.position).magnitude;
	}

	public void SetChase(bool newValue)
	{
		chase = newValue;
	}

	private void GetReferences()
	{
		playerAnimator = FindObjectOfType<PlayerAnimator>();
		Assert.IsNotNull(playerAnimator, "PlayerAnimator not found");
		anim = GetComponent<Animator>();
		Assert.IsNotNull(anim, name + "Animator not found");
		player = FindObjectOfType<Player>();
		Assert.IsNotNull(player, "Player not found");
	}
}
