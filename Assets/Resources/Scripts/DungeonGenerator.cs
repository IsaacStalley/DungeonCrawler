using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private int horizontalTotalSize;
    public int horizontalStepSize = 10;
    public int verticalTotalSize = 13;
    public int verticalStepSize = 4;
    private int floorLayer = 2;
    private float tileSize = 1.1f;
    public string[,] dungeon = new string[15, 60];
    public string[] dungeonTiles = new string[] {"Regular_1", "Regular_2", "Smooth_In", "Smooth_Out", "Regular_Corner"};

    // Start is called before the first frame update
    void Start()
    {
        GenerateHorizontalSize();
        GenerateDungeon();
        SpawnDungeon();
    }

    private void GenerateHorizontalSize()
    {
        horizontalTotalSize = Random.Range(30, 50);
    }
    private void SpawnDungeon()
    {
        for (int i = 0; i < verticalTotalSize; i++)
        {
            for (int x = 0; x < horizontalTotalSize; x++)
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
        for (int i = floorLayer; i < verticalTotalSize; i ++)
        {
            dungeon[i, 0] = dungeonTiles[0];
        }
    }

    private void AddMiddleTiles()
    {
        for (int i = 0; i < horizontalTotalSize; i++)
        {
            int stepSize = Random.Range(0, horizontalStepSize);
            AddVerticalTiles(i);
            for (int x = 0; x <= stepSize; x ++)
            {
                dungeon[floorLayer, i + x] = dungeonTiles[0];
            }
            i += stepSize;
        }
    }
    private void AddVerticalTiles(int i)
    {
        int newfloorLayer = GetNewFloorLayer();
        int x = newfloorLayer;
        while (x > floorLayer)
        {
            dungeon[x, i] = dungeonTiles[0];
            x --;
        }
        while (x < floorLayer)
        {
            dungeon[x, i] = dungeonTiles[0];
            x ++;
        }
        floorLayer = newfloorLayer;
    }

    private int GetNewFloorLayer()
    {
        return Random.Range(0, verticalStepSize);
    }

    private void AddEndTiles()
    {
        for (int i = floorLayer; i < verticalTotalSize; i++)
        {
            dungeon[i, horizontalTotalSize] = dungeonTiles[0];
        }
    }
}
