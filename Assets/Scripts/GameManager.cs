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
	Material viewMask;

	[SerializeField]
	GameObject player;

	[SerializeField]
	Transform playerStart;

	[SerializeField]
	Transform cameraStart;

	// Start is called before the first frame update
	public void Start()
	{
		SetupViewMasks();
		player.SetActive(false);
	}

    public void Update()
    {
        float gridHeight = player.transform.position.y * 2.5f;

        if ((MapGenerator.Instance.lastY - (100 * 0.2f)) < gridHeight)
        {
            MapGenerator.Instance.GenerateMap();
        }

        UIManager.Instance.UpdateScore(gridHeight);
    }

    private void SetupViewMasks()
	{
        viewMask.SetColor("_Color", Color.black);
    }

	public void StartGame()
	{
		SetupViewMasks();

        UnlockView();

        player.SetActive(true);
		player.transform.position = playerStart.position;

		Camera.main.transform.position = cameraStart.position;

		UIManager.Instance.HideMainMenu();
		UIManager.Instance.HideGameOver();
        UIManager.Instance.ClearScore();

		MapGenerator.Instance.ClearMap();
		MapGenerator.Instance.GenerateMap(true);
    }

	public void EndGame()
	{
        LockView();

        player.SetActive(false);
		UIManager.Instance.ShowGameOver();
	}

	public void UnlockView()
    {
		IEnumerator coroutine = FadeColour();
		StartCoroutine(coroutine);
    }

    public void LockView()
    {
        IEnumerator coroutine = FadeColourInverse();
        StartCoroutine(coroutine);
    }

    private IEnumerator FadeColour()
	{
		for (float t = 0.01f; t < 2f; t+=0.1f)
		{
            viewMask.color = Color.Lerp(Color.black, Color.white, t / 2f);
			yield return null;
		}
    }

    private IEnumerator FadeColourInverse()
    {
        for (float t = 0.01f; t < 2f; t += 0.1f)
        {
            viewMask.color = Color.Lerp(Color.white, Color.black, t / 2f);
            yield return null;
        }
    }
}
