using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    const float locomotionAnimationSmoothTime = .2f;

    public GameObject character;
    public GameObject shield;

    private NavMeshAgent agent;
    private Animator characterAnimator;
    private Animator shieldAnimator;
    private bool swordOut;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimator = character.GetComponent<Animator>();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        characterAnimator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
