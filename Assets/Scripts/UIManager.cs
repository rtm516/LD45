using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField]
    TextMeshProUGUI scoreText;

    int curScore = 0;
    int scoreOffset = 9;

    // Start is called before the first frame update
    public void Start()
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
        scoreText.gameObject.SetActive(true);
    }

    public void ClearScore()
    {
        UpdateScore(-scoreOffset, true);
    }

    public void UpdateScore(float playerScore, bool force = false)
    {
        int newScore = Mathf.RoundToInt(playerScore) + scoreOffset;

        if (newScore > curScore || force)
        {
            scoreText.text = "Score: " + newScore;
            curScore = newScore;
        }
    }
}
