using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Transform cameraTransform;

	[SerializeField]
	float movementSpeed = 1.0f;

	[SerializeField]
	float jumpPower = 1.0f;

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

		if (transform.position.y > camPos.y)
		{
			camPos.y = transform.position.y;
			cameraTransform.position = camPos;
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);

		rb.velocity = movement;


		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
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

	// Player went offscreen
	void OnBecameInvisible()
	{
		GameManager.Instance.EndGame();
	}
}
