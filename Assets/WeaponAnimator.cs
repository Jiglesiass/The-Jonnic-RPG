using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
	public Transform weapon;
	public Transform newParent;
	public Animator shieldAnimator;

	private Animator anim;
	private bool folded = true;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Start ()
	{
		AddEvent(0, 0.33f, "ChangeWeaponParent", newParent);
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			anim.SetBool("swordOut", true); 
		}
	}

	public void ChangeWeaponParent(GameObject newParent)
	{
		weapon.parent = newParent.transform;
		AnimateShield();
	}

	private void AnimateShield()
	{
		if (folded)
		{
			shieldAnimator.SetTrigger("unfold");
			folded = false;
		}
		else
		{
			shieldAnimator.SetTrigger("fold");
			folded = true;
		}
	}

	void AddEvent(int Clip, float time, string functionName, Object objectReferenceParameter)
	{
		AnimationEvent animationEvent = new AnimationEvent();
		animationEvent.functionName = functionName;
		animationEvent.objectReferenceParameter = objectReferenceParameter;
		animationEvent.time = time;
		AnimationClip clip = anim.runtimeAnimatorController.animationClips[Clip];
		clip.AddEvent(animationEvent);
	}
}
