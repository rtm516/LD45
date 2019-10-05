using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	#region Singleton Pattern
	public static UIManager Instance { get; private set; }
	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}
	#endregion

	[SerializeField]
	GameObject gameOverPanel;

	[SerializeField]
	GameObject mainMenuPanel;

	// Start is called before the first frame update
	void Start()
    {
		HideGameOver();
	}

	public void ShowGameOver()
	{
		gameOverPanel.SetActive(true);
	}

	public void HideGameOver()
    {
		gameOverPanel.SetActive(false);
	}

	public void HideMainMenu()
	{
		mainMenuPanel.SetActive(false);
	}
}
