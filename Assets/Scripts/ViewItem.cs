using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewItem : MonoBehaviour
{
	[SerializeField]
	int viewID = 1;

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.tag == "Player")
		{
			GameManager.Instance.UnlockView(viewID);
			gameObject.SetActive(false);
		}
	}
}
