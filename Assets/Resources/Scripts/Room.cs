using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int height;
    public int width;
    public int size = 20;
    public int[] widthRange = new int[] {8, 18};
    public int[] heightRange = new int[] {8, 12};
    public string[,] roomMatrix = new string[20,20];
    public int[,] tileRotations = new int[20,20];
    public string[] dungeonTiles = new string[] {"Regular_1", "Regular_2", "Smooth_In", "Smooth_Out", "Regular_Corner", "Regular_Corner_Inner", "Cobble"};


    private void Awake() 
    {
        width = Random.Range(widthRange[0], widthRange[1]);
        height = Random.Range(heightRange[0], heightRange[1]);
        FillArray();
        GenerateRoom(height, width);
    }

    private void FillArray()
    {
        for (int i = 0; i < size; i++)
        {
            for (int x = 0; x < size; x++)
            {
                roomMatrix[i, x] = null;
                tileRotations[i, x] = 0;
            }
        }
    }
    private void GenerateRoom(int height, int width)
    {
        int hCounter1 = Random.Range(3, (height - 2) / 2);
        int hCounter2 = Random.Range(3, (height - 2) / 2);
        int wCounter1 = Random.Range(3, (width - 1) / 2);
        int wCounter2 = Random.Range(3, (width - 1) / 2);
        int column1 = 1;
        int column2 = width - 2;
        int row1 = 1;
        int row2 = height - 2;

        int firstColumn1 = column1;
        int wStepCounter1 = firstColumn1;
        int firstColumn2 = column2;
        int wStepCounter2 = firstColumn2;

        int firstRow1 = row1;
        int hStepCounter1 = firstRow1;
        int firstRow2 = row2;
        int hStepCounter2 = firstRow2;

        for (int h = 1; h < height - 1; h++)
        {

            if (h == hCounter1 || h == height - 4)
            {
                firstColumn1 = column1;
                wStepCounter1 = firstColumn1;
                column1 = Random.Range(0,3);
                hCounter1 = Random.Range(hCounter1 + 3, height - 4);

                if (h == height - 4)
                {
                    column1 = 1;
                    hCounter1 = height;
                }
            }

            if (h == hCounter2 || h == height - 4)
            {
                firstColumn2 = column2;
                wStepCounter2 = firstColumn2;
                column2 = Random.Range(width - 3, width);
                hCounter2 = Random.Range(hCounter2 + 3, height - 4);

                if (h == height - 4)
                {
                    column2 = width - 2;
                    hCounter2 = height;
                }
            }

            roomMatrix[h, column1] = dungeonTiles[Random.Range(0,2)];
            roomMatrix[h, column2] = dungeonTiles[Random.Range(0,2)];

            tileRotations[h, column1] = -90;
            tileRotations[h, column2] = 90;

            while (wStepCounter1 < column1)
            {
                roomMatrix[h, firstColumn1] = dungeonTiles[5];
                tileRotations[h, firstColumn1] = 0;
                
                if (wStepCounter1 > firstColumn1)
                {
                roomMatrix[h, wStepCounter1] = dungeonTiles[Random.Range(0,2)];
                tileRotations[h, wStepCounter1] = 0;
                }
                if (wStepCounter1 + 1 == column1)
                {
                    roomMatrix[h, wStepCounter1 + 1] = dungeonTiles[4];
                    tileRotations[h, wStepCounter1 + 1] = 0;
                }
                wStepCounter1++;
            }

            while (wStepCounter1 > column1)
            {
                roomMatrix[h, firstColumn1] = dungeonTiles[4];
                tileRotations[h, firstColumn1] = -90;

                if (wStepCounter1 < firstColumn1)
                {
                roomMatrix[h, wStepCounter1] = dungeonTiles[Random.Range(0,2)];
                tileRotations[h, wStepCounter1] = 180;
                }
                if (wStepCounter1 - 1 == column1)
                {
                    roomMatrix[h, wStepCounter1 - 1] = dungeonTiles[5];
                    tileRotations[h, wStepCounter1 - 1] = -90;
                }
                wStepCounter1--;
            }
            while (wStepCounter2 < column2)
            {
                roomMatrix[h, firstColumn2] = dungeonTiles[4];
                tileRotations[h, firstColumn2] = 180;

                if (wStepCounter2 > firstColumn2)
                {
                roomMatrix[h, wStepCounter2] = dungeonTiles[Random.Range(0,2)];
                tileRotations[h, wStepCounter2] = 180;
                }
                if (wStepCounter2 + 1 == column2)
                {
                    roomMatrix[h, wStepCounter2 + 1] = dungeonTiles[5];
                    tileRotations[h, wStepCounter2 + 1] = 180;
                }
                wStepCounter2++;
            }

            while (wStepCounter2 > column2)
            {
                roomMatrix[h, firstColumn2] = dungeonTiles[5];
                tileRotations[h, firstColumn2] = 90;

                if (wStepCounter2 < firstColumn2)
                {
                roomMatrix[h, wStepCounter2] = dungeonTiles[Random.Range(0,2)];
                tileRotations[h, wStepCounter2] = 0;
                }
                if (wStepCounter2 - 1 == column2)
                {
                    roomMatrix[h, wStepCounter2 - 1] = dungeonTiles[4];
                    tileRotations[h, wStepCounter2 - 1] = 90;
                }
                wStepCounter2--;
            }
        }

        for (int w = 1; w < width - 1; w++)
        {
            if (w == wCounter1 || w == width - 4)
            {
                firstRow1 = row1;
                hStepCounter1 = firstRow1;
                row1 = Random.Range(0,3);
                wCounter1 = Random.Range(wCounter1 + 3, width - 4);

                if (w == width - 4)
                {
                    row1 = 1;
                    wCounter1 = width;
                }

            }

            if (w == wCounter2 || w == width - 4)
            {
                firstRow2 = row2;
                hStepCounter2 = firstRow2;
                row2 = Random.Range(height - 3, height);
                wCounter2 = Random.Range(wCounter2 + 3,  width - 4);

                if (w == width - 4)
                {
                    row2 = height - 2;
                    wCounter2 = width;
                }
            }

            roomMatrix[row1, w] = dungeonTiles[Random.Range(0,2)];
            roomMatrix[row2, w] = dungeonTiles[Random.Range(0,2)];
            tileRotations[row1, w] = 180;

            while (hStepCounter1 < row1)
            {
                roomMatrix[firstRow1, w] = dungeonTiles[5];
                tileRotations[firstRow1, w] = 180;

                if (hStepCounter1 > firstRow1)
                {
                roomMatrix[hStepCounter1, w] = dungeonTiles[Random.Range(0,2)];
                tileRotations[hStepCounter1, w] = 90;
                }
                if (hStepCounter1 + 1 == row1)
                {
                    roomMatrix[hStepCounter1 + 1, w] = dungeonTiles[4];
                    tileRotations[hStepCounter1 + 1, w] = 180;
                }
                hStepCounter1++;
            }

            while (hStepCounter1 > row1)
            {
                roomMatrix[firstRow1, w] = dungeonTiles[4];
                tileRotations[firstRow1, w] = -90;

                if (hStepCounter1 < firstRow1)
                {
                roomMatrix[hStepCounter1, w] = dungeonTiles[Random.Range(0,2)];
                tileRotations[hStepCounter1, w] = -90;
                }
                if (hStepCounter1 - 1 == row1)
                {
                    roomMatrix[hStepCounter1 - 1, w] = dungeonTiles[5];
                    tileRotations[hStepCounter1 - 1, w] = -90;
                }
                hStepCounter1--;
            }

            while (hStepCounter2 < row2)
            {
                roomMatrix[firstRow2, w] = dungeonTiles[4];

                if (hStepCounter2 > firstRow2)
                {
                roomMatrix[hStepCounter2, w] = dungeonTiles[Random.Range(0,2)];
                tileRotations[hStepCounter2, w] = -90;
                }
                if (hStepCounter2 + 1 == row2)
                {
                    roomMatrix[hStepCounter2 + 1, w] = dungeonTiles[5];
                    tileRotations[hStepCounter2 + 1, w] = 0;
                }
                hStepCounter2++;
            }

            while (hStepCounter2 > row2)
            {
                roomMatrix[firstRow2, w] = dungeonTiles[5];
                tileRotations[firstRow2, w] = 90;

                if (hStepCounter2 < firstRow2)
                {
                roomMatrix[hStepCounter2, w] = dungeonTiles[Random.Range(0,2)];
                tileRotations[hStepCounter2, w] = 90;
                }
                if (hStepCounter2 - 1 == row2)
                {
                    roomMatrix[hStepCounter2 - 1, w] = dungeonTiles[4];
                    roomMatrix[hStepCounter2 - 2, w] = dungeonTiles[2];
                    roomMatrix[hStepCounter2 - 2, w - 1] = dungeonTiles[3];
                    tileRotations[hStepCounter2 - 1, w] = 90;
                }
                hStepCounter2--;
            }

            if (w == 1 || w == width - 2)
            {
                roomMatrix[row1, w] = dungeonTiles[5];
                roomMatrix[row2, w] = dungeonTiles[5];

                tileRotations[row1, w] = 180;
                tileRotations[row2, w] = 90;

                tileRotations[1, 1] = -90;
                tileRotations[height - 2, 1] = 0;
            }
        }
    }
}
