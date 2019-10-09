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

	[SerializeField]
	GameObject viewItems;

	// Start is called before the first frame update
	public void Start()
	{
		SetupViewMasks();
		player.SetActive(false);
		viewItems.SetActive(false);
	}

	private void SetupViewMasks()
	{
		foreach (Material mask in ViewMasks)
		{
			mask.SetColor("_Color", Color.black);
		}
	}

	public void StartGame()
	{
		SetupViewMasks();

		player.SetActive(true);
		SetViewItemsVisible(true);
		player.transform.position = playerStart.position;

		Camera.main.transform.position = cameraStart.position;

		UIManager.Instance.HideMainMenu();
		UIManager.Instance.HideGameOver();

		MapGenerator.Instance.ClearMap();
		MapGenerator.Instance.GenerateMap();
    }

	public void EndGame()
	{
		player.SetActive(false);
		SetViewItemsVisible(false);
		UIManager.Instance.ShowGameOver();
	}

	private void SetViewItemsVisible(bool visible)
	{
		viewItems.SetActive(visible);
		foreach (ViewItem viewItem in viewItems.GetComponentsInChildren<ViewItem>(true))
		{
			viewItem.gameObject.SetActive(visible);
		}
	}

	public void UnlockView(int viewID)
    {
		IEnumerator coroutine = FadeColour(viewID - 1);
		StartCoroutine(coroutine);
	}

	private IEnumerator FadeColour(int viewID)
	{
		Material mat = ViewMasks[viewID];

		for (float t = 0.01f; t < 2f; t+=0.1f)
		{
			mat.color = Color.Lerp(Color.black, Color.white, t / 2f);
			yield return null;
		}
	}
}
