using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]

	[SerializeField]
	float movementSpeed = 1.0f;

	[SerializeField]
	float jumpPower = 1.0f;


	[Header("Ground detection")]

	[SerializeField]
	LayerMask groundLayers;

	[SerializeField]
	Transform topLeft;

	[SerializeField]
	Transform bottomRight;


	[Header("Other")]

	[SerializeField]
	Transform cameraTransform;

	Rigidbody2D rb;

	bool isGrounded = false;

	int layerMask;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();

		layerMask =~ LayerMask.NameToLayer("Ground");
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

		isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayers);

		/*if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.3f, layerMask))
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}*/


		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		}
	}

	/*void OnCollisionEnter2D(Collision2D theCollision)
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
	}*/

	// Player went offscreen
	void OnBecameInvisible()
	{
		GameManager.Instance.EndGame();
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 topRight = topLeft.position;
		topRight.x = bottomRight.position.x;

		Vector3 bottomLeft = topLeft.position;
		bottomLeft.y = bottomRight.position.y;

		Gizmos.color = Color.red;

		Gizmos.DrawLine(topLeft.position, topRight);
		Gizmos.DrawLine(topRight, bottomRight.position);

		Gizmos.DrawLine(topLeft.position, bottomLeft);
		Gizmos.DrawLine(bottomLeft, bottomRight.position);
	}
}
