using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Transform cameraTransform;

	[SerializeField]
	float movementSpeed = 1.0f;

	Rigidbody2D rb;

	bool isGrounded = false;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update()
	{
		Vector3 camPos = cameraTransform.position;
		camPos.x = transform.position.x;

		cameraTransform.position = camPos;
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);

		rb.velocity = movement;


		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.tag == "Ground")
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.tag == "Ground")
		{
			isGrounded = false;
		}
	}
}
