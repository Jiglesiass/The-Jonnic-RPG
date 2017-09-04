using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Collections;

public enum PlayerState { attacking, idle, battleStance }

public class PlayerAnimator : MonoBehaviour
{
	public Transform weapon;
	public Transform newParent;
	public Animator shieldAnimator;

	private NavMeshAgent agent;      
	private Animator anim;
	private static PlayerState playerState;

	[SerializeField]
	private Vector3 rightHandPos, rightHandRot;
	private Vector3 shieldPos, shieldRot;   // pos and rot relative to the shield

	private bool swordOut;
	private bool startCounting;
	private float drawTime = 0.33f;
	private float putBackTime = 0.75f;       
	private float foldTime = 0.4f;

	private bool freezePosition;
	private Vector3 positionBeforeAttack;

    const float locomotionAnimationSmoothTime = .2f;   // smooth time

    private void Awake()
	{
		agent = GetComponentInParent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}

	void Start ()
	{
		playerState = PlayerState.idle;

        shieldPos = weapon.transform.localPosition;     // get the starting pos and rot of the weapon
		shieldRot = weapon.transform.localRotation.eulerAngles;
	}
	
	void Update ()
	{
		Debug.Log(playerState);

        float speedPercent = agent.velocity.magnitude / agent.speed;    // interaction between agent and animator
        anim.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

		#region DrawSheathSword
		if (Input.GetKeyDown(KeyCode.V))
		{
			playerState = (swordOut) ? PlayerState.idle : PlayerState.battleStance;
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

		#region BasicAttack

		if (Input.GetMouseButton(0) && (playerState != PlayerState.attacking && playerState == PlayerState.battleStance))
		{
			IEnumerator coroutine = SwitchToAttackStance(0.5f);
			StartCoroutine(coroutine);
			Attack();
		}
		else if (Input.GetMouseButtonUp(0) && playerState == PlayerState.attacking)
		{
			anim.ResetTrigger("attack");
		}
		if (freezePosition)
		{
			FreezePosition();
		}

		#endregion

		#region Sidesteps

		if (Input.GetKeyDown(KeyCode.Space))
		{
			agent.isStopped = true;
			anim.SetTrigger("spin");
		}

		#endregion
	}

	private void Attack()
	{
		agent.destination = transform.parent.position;
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
		{
			transform.parent.DOLookAt(hit.point, 0.15f, AxisConstraint.Y);
		}
		anim.SetTrigger("attack");
	}

	private void ChangeWeaponParent(Transform newParent)
	{
		weapon.parent = newParent.transform;
		AnimateShield();
	}

	private void AnimateShield()
	{
		shieldAnimator.SetBool("swordOut", swordOut);
	}

	private void SetFreezePosition(int value)
	{
		freezePosition = (value != 0);
		if (freezePosition) { GetCurrentPosition(); }
	}

	private void GetCurrentPosition()
	{
		positionBeforeAttack = transform.parent.position;
	}

	private void FreezePosition()
	{
		transform.parent.position = positionBeforeAttack;
	}

	public static PlayerState GetPlayerState()
	{
		return playerState;
	}

	private IEnumerator SwitchToAttackStance(float time)
	{
		playerState = PlayerState.attacking;
		yield return new WaitForSeconds(time);
		playerState = PlayerState.battleStance;
	}
}
