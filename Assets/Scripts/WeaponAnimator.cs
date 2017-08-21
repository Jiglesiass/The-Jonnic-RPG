using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
	public Transform weapon;
	public Transform newParent;
	public Animator shieldAnimator;

	private Animator anim;
	private bool swordOut;
	[SerializeField]
	private Vector3 rightHandPos, rightHandRot;
	private Vector3 leftHandPos, leftHandRot;

	private bool startCounting;
	private float time = 0.33f;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Start ()
	{
		leftHandPos = transform.position;
		leftHandRot = transform.rotation.eulerAngles;
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			swordOut = !swordOut;
			anim.SetBool("swordOut", swordOut);
			startCounting = true;
		}

		if (startCounting)
		{
			time -= Time.deltaTime;
			if (time <= 0)
			{
				Debug.Log("Changing parent");
				ChangeWeaponParent(newParent.gameObject);
				weapon.localPosition = rightHandPos;
				Quaternion rot = Quaternion.Euler(rightHandRot.x, rightHandRot.y, rightHandRot.z);
				weapon.localRotation = rot;
				startCounting = false;
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
