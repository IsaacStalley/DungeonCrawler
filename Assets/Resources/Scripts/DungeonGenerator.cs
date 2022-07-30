using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private Vector3 playerPosition;
    public int dungeonSize = 400;
    private float tileSize = 1;
    private int roomCount = 10;
    Room[,] totalRooms = new Room[10, 10];
    public string[,] dungeon = new string[400, 400];
    public int[,] tileRotations = new int[400, 400];
    int[][] tunnelOpenings = new int[10][];
    int[][] tunnelOpeningPositions = new int[10][];

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        FillDungeonList();
        GenerateDungeon();
        SpawnDungeon();
    }

    private void FillDungeonList() 
    {
        for (int i = 0; i < dungeonSize; i++)
        {
            for (int x = 0; x < dungeonSize; x++)
            {
                dungeon[i,x] = null;
                tileRotations[i, x] = 0;
            }
        }

        for (int i = 0; i < roomCount; i++)
        {
            tunnelOpenings[i] = new int[] {0,0,0,0};
            tunnelOpeningPositions[i] = new int[] {0,0,0,0,0,0,0,0};
            for (int x = 0; x < roomCount; x++)
            {
                //totalRooms[i,x] = new int[] {0,0,0};
            }
        }
    }

    private void GenerateDungeon()
    {
        GenerateRooms();

    }

    private void GenerateRooms()
    {
        int widthSizeCounter = 0;
        int[] heightSizeCounter = new int[3] {0,0,0};

        for (int i = 0; i < 1; i++)
        {
            widthSizeCounter = 0;

            for (int x = 0; x < 1; x++)
            {
                GameObject roomObj = Resources.Load<GameObject>("Prefabs/room");
                roomObj = Instantiate(roomObj);
                roomObj.name = "room" + i.ToString() + x.ToString();
                Room room = roomObj.GetComponent<Room>();

                totalRooms[i, x] = room;

                for (int height = 0; height < room.height; height++)
                {
                    for (int width = 0; width < room.width; width++)
                    {
                        dungeon[height + heightSizeCounter[x], width + widthSizeCounter] = room.roomMatrix[height, width];
                        tileRotations[height + heightSizeCounter[x], width + widthSizeCounter] = room.tileRotations[height, width];
                    }
                }

                widthSizeCounter += room.width + 7;
                heightSizeCounter[x] += room.height + 7;
            }
        }
    }

    private void GenerateTunnels()
    {
        int[] start = new int[] {0, 0};
        int[] end = new int[] {0, 0};

        for (int i = 0; i < 9; i++)
        {
            if (tunnelOpenings[i][1] == 1)
            {
                start[0] = tunnelOpeningPositions[i][2];
                start[1] = tunnelOpeningPositions[i][3];
                end[0] = tunnelOpeningPositions[i + 1][6];
                end[1] = tunnelOpeningPositions[i + 1][7];
                List<int[]> path = TunnelBFS(start, end);
                for (int x = 0; x < path.Count; x++)
                {
                    dungeon[path[x][0], path[x][1]] = "Smooth_In";
                }
            }

            if (tunnelOpenings[i][2] == 1)
            {
                start[0] = tunnelOpeningPositions[i][4];
                start[1] = tunnelOpeningPositions[i][5];
                end[0] = tunnelOpeningPositions[i + 1][0];
                end[1] = tunnelOpeningPositions[i + 1][1];
                List<int[]> path = TunnelBFS(start, end);
                for (int x = 0; x < path.Count; x++)
                {
                    dungeon[path[x][0], path[x][1]] = "Smooth_In";
                }
            }
        }
    }

    private List<int[]> TunnelBFS(int[] start, int[] end)
    {
        Queue<int[]> queue = new Queue<int[]>();
        int[] adress = new int[] {start[0], start[1]};
        List<int[]> neighbors = new List<int[]>();
        List<int[]> path = new List<int[]>();
        int[,][] previous = new int[dungeonSize, dungeonSize][];
        bool[,] visited = new bool[dungeonSize, dungeonSize];

        for (int i = 0; i < 5; i++)
            neighbors.Add(new int[] {0, 0});

        for (int i = 0; i < dungeonSize; i++)
        {
            for (int x = 0; x < dungeonSize; x++)
            {
                visited[i,x] = false;
                previous[i,x] = new int[] {0, 0};
            }
        }

        queue.Enqueue(start);
        visited[start[0], start[1]] = true;
         
        while (queue.Count != 0)
        {
            adress = queue.Dequeue();
            neighbors[0] = new int[] {adress[0] + 1, adress[1]};
            neighbors[1] = new int[] {adress[0], adress[1] - 1};
            neighbors[2] = new int[] {adress[0], adress[1] + 1};
            neighbors[3] = new int[] {adress[0] + 1, adress[1] + 1};
            neighbors[4] = new int[] {adress[0] + 1, adress[1] - 1};
            
            for (int i = 0; i < 5; i++)
            {
                if (neighbors[i][0] != dungeonSize - 1 && neighbors[i][1] != dungeonSize - 1 && neighbors[i][0] > 0 && neighbors[i][1] > 0)
                {
                    if (!visited[neighbors[i][0], neighbors[i][1]] && dungeon[neighbors[i][0], neighbors[i][1]] == null)
                    { 
                        queue.Enqueue(new int[] {neighbors[i][0], neighbors[i][1]});
                        visited[neighbors[i][0], neighbors[i][1]] = true;
                        previous[neighbors[i][0], neighbors[i][1]] = adress;
                    }
                }
                if (neighbors[i][0] == end[0] && neighbors[i][1] == end[1])
                {
                    visited[neighbors[i][0], neighbors[i][1]] = true;
                    previous[neighbors[i][0], neighbors[i][1]] = adress;
                    queue.Clear();
                }
            }
        }

        int[] current = end;
        while (visited[current[0], current[1]])
        {
            path.Add(current);
            current = previous[current[0], current[1]];
        }
        return path;
    }

    private void SpawnDungeon()
    {
        for (int i = 0; i < dungeonSize; i++)
        {
            for (int x = 0; x < dungeonSize; x++)
            {
                InstantiateTile(i, x);
            }
        }
    }

    private void InstantiateTile(int i, int x)
    {
        if (dungeon[i, x] != null)
        {
            GameObject tile = Resources.Load<GameObject>("Prefabs/" + dungeon[i,x]);
            Instantiate(tile, GetTilePosition(x, i), Quaternion.Euler(new Vector3(0f, 0f, GetTileRotation(i, x))));
        }
    }

    private Vector3 GetTilePosition(int row, int column)
    {
        return new Vector3(tileSize * row + 1, tileSize * (-1*column - 4.5f), 0f);
    }

    private float GetTileRotation(int row, int column)
    {   
        return tileRotations[row, column];
    }
}
