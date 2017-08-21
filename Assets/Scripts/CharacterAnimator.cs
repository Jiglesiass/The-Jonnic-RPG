using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    const float locomotionAnimationSmoothTime = .2f;

    public GameObject character;
    public GameObject shield;
    public Transform weapon;
    public Transform rightHand;

    private NavMeshAgent agent;
    private Animator characterAnimator;
    private Animator shieldAnimator;
    private bool swordOut;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimator = character.GetComponent<Animator>();
        shieldAnimator = shield.GetComponent<Animator>();
        swordOut = false;
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        characterAnimator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (swordOut == false)
            {
                swordOut = true;
            }
            else
            {
                swordOut = false;
            }

            characterAnimator.SetBool("swordOut", swordOut);
            shieldAnimator.SetBool("swordOut", swordOut);
        }
    }

    public void ChangeWeaponParent(GameObject newParent)
    {
        weapon.parent = newParent.transform;
        //  AnimateShield();
    }
}
