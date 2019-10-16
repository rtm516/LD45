using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
	#region Singleton Pattern
	public static MapGenerator Instance { get; private set; }
	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}
	#endregion

	[Header("Tilemap settings")]
	[SerializeField]
	Tile wallTile;

	[SerializeField]
	Tile floorTile;

	[SerializeField]
	Tilemap[] tilemaps;

	[SerializeField]
	Tilemap wallTilemap;

	[SerializeField]
	Vector3Int bottomLeft = new Vector3Int();


	[Header("Generator settings")]

	[SerializeField]
	int minHeightDiff = 2;

	[SerializeField]
	int maxHeightDiff = 3;

	[SerializeField]
	int minHoriz = 0;

	[SerializeField]
	int maxHoriz = 20;

	[SerializeField]
	int minSize = 1;

	[SerializeField]
	int maxSize = 6;

	public int lastY = 1;

	public void Start()
	{
		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int curTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;

		Random.InitState(curTime);
	}

	public void ClearMap()
	{
		foreach (Tilemap tilemap in tilemaps)
		{
			tilemap.ClearAllTiles();
		}

		// Bottom
		BoxFill(wallTilemap, wallTile, bottomLeft + new Vector3Int(0, -1, 0), bottomLeft + new Vector3Int(maxHoriz, -1, 0));
	}

	public void GenerateMap(bool newMap = false)
	{
        //BoxFill(GetTilemap(3), floorTile, bottomLeft + new Vector3Int(0, 15, 0), bottomLeft + new Vector3Int(maxHoriz, 15, 0));

        int lastPlatStart = 0;
        int lastPlatEnd = 0;
        int lastPlatY = 0;

        int maxDist = 10;

        if (newMap)
        {
            lastY = 1;
        }



        // Left wall
        BoxFill(wallTilemap, wallTile, bottomLeft + new Vector3Int(-1, lastY - 2, 0), bottomLeft + new Vector3Int(-1, lastY + 100, 0));
        // Right wall
        BoxFill(wallTilemap, wallTile, bottomLeft + new Vector3Int(maxHoriz, lastY - 2, 0), bottomLeft + new Vector3Int(maxHoriz, lastY + 100, 0));


        int y = lastY;
		while (y < lastY + 100)
		{
			int platformSize = Random.Range(minSize, maxSize + 1);
			int platformPos = Random.Range(minHoriz, maxHoriz);
			if (platformPos + platformSize > maxHoriz)
			{
				platformSize -= ((platformPos + platformSize) - maxHoriz);
			}


            if (Mathf.Abs(platformPos - lastPlatStart) > maxDist && Mathf.Abs((platformPos + platformSize) - lastPlatEnd) > maxDist)
            {
                continue;
            }

			BoxFill(GetTilemap(1), floorTile, bottomLeft + new Vector3Int(platformPos, y, 0), bottomLeft + new Vector3Int(platformPos + platformSize, y, 0));

            lastPlatStart = platformPos;
            lastPlatEnd = platformPos + platformSize;
            lastPlatY = y;

            y += Random.Range(minHeightDiff, maxHeightDiff + 1);
        }

        lastY = y;
	}
	private Tilemap GetTilemap(int viewID)
	{
		return tilemaps[viewID - 1];
	}

	private void BoxFill(Tilemap map, TileBase tile, Vector3Int start, Vector3Int end)
	{
		//Determine directions on X and Y axis
		var xDir = start.x < end.x ? 1 : -1;
		var yDir = start.y < end.y ? 1 : -1;

		//How many tiles on each axis?
		int xCols = Mathf.Abs(start.x - end.x);
		if (xCols < 1) { xCols = 1; }

		int yCols = Mathf.Abs(start.y - end.y);
		if (yCols < 1) { yCols = 1; }

		//Start painting
		for (var x = 0; x < xCols; x++)
		{
			for (var y = 0; y < yCols; y++)
			{
				var tilePos = start + new Vector3Int(x * xDir, y * yDir, 0);
				map.SetTile(tilePos, tile);
			}
		}
	}
}
