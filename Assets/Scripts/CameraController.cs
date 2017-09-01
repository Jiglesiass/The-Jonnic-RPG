using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;

	[SerializeField]
	private float zoomSpeed = 3f;
	private Vector3 distanceToPlayer;

	private void Start()
	{
		Camera.main.transform.LookAt(player.position + Vector3.up);

		distanceToPlayer = transform.position - player.position;
	}

	void Update ()
	{
		transform.position = player.position + distanceToPlayer;

		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

		if (mouseWheel != 0)
		{
			transform.Translate(Vector3.forward * mouseWheel * zoomSpeed, Space.Self);
			distanceToPlayer = transform.position - player.position;
		}
	}
}
