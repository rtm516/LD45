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

	// Start is called before the first frame update
	void Start()
    {
		foreach (Material mask in ViewMasks)
		{
			mask.SetColor("_Color", Color.black);
		}

		UnlockView(1);
	}

    void UnlockView(int viewID)
    {
		ViewMasks[viewID - 1].SetColor("_Color", Color.white);
    }
}
