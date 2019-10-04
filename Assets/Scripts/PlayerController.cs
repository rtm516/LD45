using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Transform cameraTransform;

	[SerializeField]
	float movementSpeed = 1.0f;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float oldHeight = transform.localScale.y;

		transform.localScale = transform.localScale + (new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime);

		float newHeight = transform.localScale.y;

		float heightDiff = (newHeight - oldHeight) * 0.03125f;

		Vector3 plyPos = transform.localPosition;
		plyPos.y += heightDiff;
		//transform.localPosition = plyPos;

		Vector3 camPos = cameraTransform.position;
		camPos.x = transform.position.x;

		cameraTransform.position = camPos;
		

		if (Input.GetAxis("Horizontal") != 0)
		{
			GetComponent<Rigidbody2D>().MovePosition(transform.position + (new Vector3(1, 0) * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed));
		}
	}
}
