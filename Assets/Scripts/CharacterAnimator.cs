using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

	public Transform weapon;

	const float locomotionAnimationSmoothTime = .2f;

    private NavMeshAgent agent;
    private Animator animator;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update ()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}

	public void ChangeWeaponParent(GameObject newParent)
	{
		weapon.parent = newParent.transform;
	}
}
