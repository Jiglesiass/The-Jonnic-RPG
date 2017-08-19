using UnityEngine;

public class Skeleton : MonoBehaviour
{
	public AnimationClip attack;

	private Animation anim;

	void Start ()
	{
		anim = GetComponent<Animation>();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.clip = attack;
			anim.Play();
		}
	}
}
