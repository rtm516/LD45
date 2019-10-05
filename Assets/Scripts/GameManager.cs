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

	[SerializeField]
	Transform playerStart;

	[SerializeField]
	Transform cameraStart;

	// Start is called before the first frame update
	void Start()
    {
		StartGame();
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

	public void StartGame()
	{
		SetupViewMasks();

		player.SetActive(true);
		player.transform.position = playerStart.position;

		Camera.main.transform.position = cameraStart.position;

		UIManager.Instance.HideGameOver();
	}

	public void EndGame()
	{
		player.SetActive(false);
		UIManager.Instance.ShowGameOver();
	}

	public void UnlockView(int viewID)
    {
		ViewMasks[viewID - 1].SetColor("_Color", Color.white);
    }
}
