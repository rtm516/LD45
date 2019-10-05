using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton Pattern
	public static GameManager Instance { get; private set; }
	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}
	#endregion

	GameObject mask;

	[SerializeField]
	Material[] ViewMasks;

	[SerializeField]
	GameObject player;

	// Start is called before the first frame update
	void Start()
    {
		SetupViewMasks();
	}

	private void SetupViewMasks()
	{
		foreach (Material mask in ViewMasks)
		{
			mask.SetColor("_Color", Color.black);
		}

		UnlockView(1);
		//UnlockView(2);
	}

	public void EndGame()
	{
		Debug.Log("End game!");
		Application.Quit();
	}

	public void UnlockView(int viewID)
    {
		ViewMasks[viewID - 1].SetColor("_Color", Color.white);
    }
}
