using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int horizontalMax = 50;
    public int verticalMax = 13;
    private int floorLayer = 2;
    private float tileSize = 1.1f;
    public string[,] dungeon = new string[15, 50];
    public string[] dungeonTiles = new string[] {"Regular_1", "Regular_2", "Smooth_In", "Smooth_Out", "Regular_Corner"};

    // Start is called before the first frame update
    void Start()
    {
        GenerateDungeon();
        SpawnDungeon();
    }
    private void SpawnDungeon()
    {
        for (int i = 0; i < verticalMax; i++)
        {
            for (int x = 0; x < horizontalMax; x++)
            {
                InstantiateTile(i, x);
            }
        }
    }

    private void InstantiateTile(int i, int x)
    {
        if (dungeon[i, x] != null)
        {
            GameObject tile = Resources.Load<GameObject>("Prefabs/" + dungeon[i, x]);
            Instantiate(tile, GetTilePosition(x, i), Quaternion.Euler(new Vector3(0f, 0f, GetTileRotation())));
        }
    }

    private Vector3 GetTilePosition(int x, int y)
    {
        return new Vector3(tileSize * x + 1, tileSize * (y - 4.5f), 0f);
    }

    private float GetTileRotation()
    {
        return 0f;
    }

    private void GenerateDungeon()
    {
        AddInitialTiles();
        AddMiddleTiles();
        AddEndTiles();
    }

    private void AddInitialTiles()
    {
        for (int i = floorLayer; i < verticalMax; i ++)
        {
            dungeon[i, 0] = dungeonTiles[0];
        }
    }

    private void AddMiddleTiles()
    {
        for (int i = 0; i < horizontalMax; i++)
        {
            
            dungeon[floorLayer, i] = dungeonTiles[0];
        }
    }

    private void AddEndTiles()
    {
        for (int i = 2; i < verticalMax; i++)
        {
            dungeon[i, 0] = dungeonTiles[0];
        }
    }
}
