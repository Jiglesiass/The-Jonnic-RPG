using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
	public Transform weapon;
	public Transform newParent;
	public Animator shieldAnimator;

	private NavMeshAgent agent;      //need agent of parent
	private Animator anim;

	[SerializeField]
	private Vector3 rightHandPos, rightHandRot;
	private Vector3 shieldPos, shieldRot;   //pos and rot relative to the shield

	private bool freezePosition;
	private Vector3 positionBeforeAttack;
	private bool swordOut;
	private bool startCounting;
	private float drawTime = 0.33f;
    private float putBackTime = 0.75f;       //longer time for PutBackAnimation
    private float foldTime = 0.4f;      //to control time at which shield gets folded
    const float locomotionAnimationSmoothTime = .2f; //smooth time

    private void Awake()
	{
		agent = GetComponentInParent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}

	void Start ()
	{
        shieldPos = weapon.transform.localPosition;     //get the starting pos and rot of the weapon
		shieldRot = weapon.transform.localRotation.eulerAngles;
	}
	
	void Update ()
	{
        float speedPercent = agent.velocity.magnitude / agent.speed;    //interaction between agent and animator
        anim.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

		#region DrawSheathSword
		if (Input.GetKeyDown(KeyCode.V))
		{
			swordOut = !swordOut;
			anim.SetBool("swordOut", swordOut);
			startCounting = true;
		}

        if (swordOut)       //check if weapon gets drawn
        {
		    if (startCounting)
		    {
			    drawTime -= Time.deltaTime;
			    if (drawTime <= 0)
			    {
				    ChangeWeaponParent(newParent);
				    weapon.localPosition = rightHandPos;
				    Quaternion rot = Quaternion.Euler(rightHandRot.x, rightHandRot.y, rightHandRot.z);
				    weapon.localRotation = rot;
				    startCounting = false;
                    drawTime = 0.33f;       //need to reset timer
}
		    }
        }
        else if (!swordOut) //check if weapon gets put back
        {
            if (startCounting)
            {
                putBackTime -= Time.deltaTime;
                foldTime -= Time.deltaTime;
                if (foldTime <= 0)    //shield animation should play earlier, now is a bit random
                {
                    AnimateShield();
                }
                if (putBackTime <= 0)
                {
                    ChangeWeaponParent(shieldAnimator.transform);      //AnimateShield method in ChangeWeaponParent doesn't play animation because animation already played
                    weapon.localPosition = shieldPos;
                    Quaternion rot = Quaternion.Euler(shieldRot.x, shieldRot.y, shieldRot.z);
                    weapon.localRotation = rot;
                    startCounting = false;
                    putBackTime = 0.75f;
                    foldTime = 0.4f;
                }
            }
        }
		#endregion

		if (Input.GetMouseButton(0))
		{
			Attack();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			anim.ResetTrigger("attack");
		}

		if (freezePosition)
		{
			FreezePosition();
		}
	}

	private void Attack()
	{
		anim.SetTrigger("attack");
	}

	private void ChangeWeaponParent(Transform newParent)
	{
		weapon.parent = newParent.transform;
		AnimateShield();
	}

	private void SetFreezePosition(int value)
	{
		freezePosition = (value == 0) ? false : true;
		if (freezePosition) { GetCurrentPosition(); }
	}

	private void FreezePosition()
	{
		Debug.Log("FreezePosition called");
		transform.parent.position = positionBeforeAttack;
	}

	private void GetCurrentPosition()
	{
		positionBeforeAttack = transform.parent.position;
	}

	private void AnimateShield()
	{
		shieldAnimator.SetBool("swordOut", swordOut);
	}
}
