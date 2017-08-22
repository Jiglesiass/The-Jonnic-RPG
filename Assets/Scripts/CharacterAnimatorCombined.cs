using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimatorCombined : MonoBehaviour
{
	public Transform weapon;
	public Transform newParent;
	public Animator shieldAnimator;
    public NavMeshAgent agent;      //need agent of parent

	private Animator anim;
	private bool swordOut;
	[SerializeField]
	private Vector3 rightHandPos, rightHandRot;
	private Vector3 shieldPos, shieldRot;   //pos and rot relative to the shield

	private bool startCounting;
	private float drawTime = 0.33f;
    private float putBackTime = 0.7f;       //longer time for PutBackAnimation
    private float foldTime = 0.4f;      //to control time at which shield gets folded
    const float locomotionAnimationSmoothTime = .2f; //smooth time

    private void Awake()
	{
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
				    ChangeWeaponParent(newParent.gameObject);
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
                    ChangeWeaponParent(shieldAnimator.gameObject);      //AnimateShield method in ChangeWeaponParent doesn't play animation because animation already played
                    weapon.localPosition = shieldPos;
                    Quaternion rot = Quaternion.Euler(shieldRot.x, shieldRot.y, shieldRot.z);
                    weapon.localRotation = rot;
                    startCounting = false;
                    putBackTime = 0.7f;
                    foldTime = 0.4f;
                }
            }
        }
	}

	private void ChangeWeaponParent(GameObject newParent)
	{
		weapon.parent = newParent.transform;
		AnimateShield();
	}

	private void AnimateShield()
	{
		shieldAnimator.SetBool("swordOut", swordOut);
	}
}
