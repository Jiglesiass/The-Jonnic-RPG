using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;
	public Transform cameraHolder;

	[SerializeField]
	private float zoomSpeed = 3f;

	private void Start()
	{
		Camera.main.transform.LookAt(cameraHolder.position + Vector3.up);
	}

	void Update ()
	{
		cameraHolder.rotation = Quaternion.identity;

		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

		if (mouseWheel != 0)
		{
			transform.Translate(Vector3.forward * mouseWheel * zoomSpeed);
		}
	}
}
