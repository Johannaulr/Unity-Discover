using UnityEngine;

public class MoveAndScaleObject : MonoBehaviour
{
	[Tooltip("Speed at which the object moves on the Z axis")]
	public float moveSpeed = 1f;

	[Tooltip("Speed at which the object scales")]
	public float scaleSpeed = 1f;

	private void Update()
	{
		if (Input.GetKey(KeyCode.S) || (OVRInput.Get(OVRInput.Button.Two)))
		{
			transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.W) || (OVRInput.Get(OVRInput.Button.One)))
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D) || (OVRInput.Get(OVRInput.Button.Three)))
		{
			transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.A) || (OVRInput.Get(OVRInput.Button.Four)))
		{
			transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
		}
	}
}