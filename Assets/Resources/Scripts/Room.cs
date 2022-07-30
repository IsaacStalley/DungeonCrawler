using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int height;
    public int width;
    public int size = 100;
    public int[] widthRange = new int[] {50, 70};
    public int[] heightRange = new int[] {8, 12};
    public string[,] roomMatrix = new string[100,100];
    public int[,] tileRotations = new int[100,100];
    public string[] edgeTiles = new string[] {"Regular_1", "Regular_2", "Cobble", "Regular_Corner", "Regular_Corner_Inner"};
    public string[] innerTiles = new string[] {"Smooth_In", "Smooth_Out", "Cobble"};
    public string[] backgroundTiles = new string[] {"Background Reg 1", "Background Reg 2", "Cobble", "Background Reg Corner", "Background Reg Corner Inner"};


    private void Awake() 
    {
        width = Random.Range(widthRange[0], widthRange[1]);
        height = Random.Range(heightRange[0], heightRange[1]);
        FillArray();
        GenerateRoom(height, width);
        AddTraps();
        AddBackground();
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
        int wCounter1 = Random.Range(3, 6);
        int wCounter2 = Random.Range(3, 6);
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

            roomMatrix[h, column1] = RandomEdgeTile();
            roomMatrix[h, column2] = RandomEdgeTile();

            tileRotations[h, column1] = -90;
            tileRotations[h, column2] = 90;

            while (wStepCounter1 < column1)
            {
                roomMatrix[h, firstColumn1] = edgeTiles[4];
                tileRotations[h, firstColumn1] = 0;
                
                if (wStepCounter1 > firstColumn1)
                {
                roomMatrix[h, wStepCounter1] = RandomEdgeTile();
                tileRotations[h, wStepCounter1] = 0;
                }
                if (wStepCounter1 + 1 == column1)
                {
                    roomMatrix[h, wStepCounter1 + 1] = edgeTiles[3];
                    tileRotations[h, wStepCounter1 + 1] = 0;
                }
                wStepCounter1++;
            }

            while (wStepCounter1 > column1)
            {
                roomMatrix[h, firstColumn1] = edgeTiles[3];
                tileRotations[h, firstColumn1] = -90;

                if (wStepCounter1 < firstColumn1)
                {
                roomMatrix[h, wStepCounter1] = RandomEdgeTile();
                tileRotations[h, wStepCounter1] = 180;
                }
                if (wStepCounter1 - 1 == column1)
                {
                    roomMatrix[h, wStepCounter1 - 1] = edgeTiles[4];
                    tileRotations[h, wStepCounter1 - 1] = -90;
                }
                wStepCounter1--;
            }
            while (wStepCounter2 < column2)
            {
                roomMatrix[h, firstColumn2] = edgeTiles[3];
                tileRotations[h, firstColumn2] = 180;

                if (wStepCounter2 > firstColumn2)
                {
                roomMatrix[h, wStepCounter2] = RandomEdgeTile();
                tileRotations[h, wStepCounter2] = 180;
                }
                if (wStepCounter2 + 1 == column2)
                {
                    roomMatrix[h, wStepCounter2 + 1] = edgeTiles[4];
                    tileRotations[h, wStepCounter2 + 1] = 180;
                }
                wStepCounter2++;
            }

            while (wStepCounter2 > column2)
            {
                roomMatrix[h, firstColumn2] = edgeTiles[4];
                tileRotations[h, firstColumn2] = 90;

                if (wStepCounter2 < firstColumn2)
                {
                roomMatrix[h, wStepCounter2] = RandomEdgeTile();
                tileRotations[h, wStepCounter2] = 0;
                }
                if (wStepCounter2 - 1 == column2)
                {
                    roomMatrix[h, wStepCounter2 - 1] = edgeTiles[3];
                    tileRotations[h, wStepCounter2 - 1] = 90;
                }
                wStepCounter2--;
            }
        }

        for (int w = 1; w < width - 1; w++)
        {
            if (w == wCounter1 || w >= width - 4)
            {
                firstRow1 = row1;
                hStepCounter1 = firstRow1;
                row1 = Random.Range(0,3);
                wCounter1 = Random.Range(wCounter1 + 3, wCounter1 + 5);

                if (w >= width - 4)
                {
                    row1 = 1;
                    wCounter1 = width;
                }
            }

            if (w == wCounter2 || w >= width - 4)
            {
                firstRow2 = row2;
                hStepCounter2 = firstRow2;
                row2 = Random.Range(height - 3, height);
                wCounter2 = Random.Range(wCounter2 + 3,  wCounter2 + 5);

                if (w >= width - 4)
                {
                    row2 = height - 2;
                    wCounter2 = width;
                }
            }

            roomMatrix[row1, w] = RandomEdgeTile();
            roomMatrix[row2, w] = RandomEdgeTile();
            tileRotations[row1, w] = 180;

            while (hStepCounter1 < row1)
            {
                roomMatrix[firstRow1, w] = edgeTiles[4];
                tileRotations[firstRow1, w] = 180;

                if (hStepCounter1 > firstRow1)
                {
                roomMatrix[hStepCounter1, w] = RandomEdgeTile();
                tileRotations[hStepCounter1, w] = 90;
                }
                if (hStepCounter1 + 1 == row1)
                {
                    roomMatrix[hStepCounter1 + 1, w] = edgeTiles[3];
                    tileRotations[hStepCounter1 + 1, w] = 180;
                }
                hStepCounter1++;
            }

            while (hStepCounter1 > row1)
            {
                roomMatrix[firstRow1, w] = edgeTiles[3];
                tileRotations[firstRow1, w] = -90;

                if (hStepCounter1 < firstRow1)
                {
                roomMatrix[hStepCounter1, w] = RandomEdgeTile();
                tileRotations[hStepCounter1, w] = -90;
                }
                if (hStepCounter1 - 1 == row1)
                {
                    roomMatrix[hStepCounter1 - 1, w] = edgeTiles[4];
                    tileRotations[hStepCounter1 - 1, w] = -90;
                }
                hStepCounter1--;
            }

            while (hStepCounter2 < row2)
            {
                roomMatrix[firstRow2, w] = edgeTiles[3];
                GenerateExtraCornerTile(firstRow2 - 1, w);

                if (hStepCounter2 > firstRow2)
                {
                roomMatrix[hStepCounter2, w] = RandomEdgeTile();
                tileRotations[hStepCounter2, w] = -90;
                }
                if (hStepCounter2 + 1 == row2)
                {
                    roomMatrix[hStepCounter2 + 1, w] = edgeTiles[4];
                    tileRotations[hStepCounter2 + 1, w] = 0;
                }
                hStepCounter2++;
            }

            while (hStepCounter2 > row2)
            {
                roomMatrix[firstRow2, w] = edgeTiles[4];
                tileRotations[firstRow2, w] = 90;

                if (hStepCounter2 < firstRow2)
                {
                roomMatrix[hStepCounter2, w] = RandomEdgeTile();
                tileRotations[hStepCounter2, w] = 90;
                }
                if (hStepCounter2 - 1 == row2)
                {
                    roomMatrix[hStepCounter2 - 1, w] = edgeTiles[3];
                    tileRotations[hStepCounter2 - 1, w] = 90;
                    GenerateExtraCornerTile(hStepCounter2 - 2, w);
                }
                hStepCounter2--;
            }

            if (w == 1 || w == width - 2)
            {
                roomMatrix[row1, w] = edgeTiles[4];
                roomMatrix[row2, w] = edgeTiles[4];

                tileRotations[row1, w] = 180;
                tileRotations[row2, w] = 90;

                tileRotations[1, 1] = -90;
                tileRotations[height - 2, 1] = 0;
            }
        }
    }

    private void GenerateExtraCornerTile(int h, int w)
    {
        if (!EdgeTester(h - 2, w) && !EdgeTester(h - 2, w - 1) && !EdgeTester(h - 2, w + 1) && !EdgeTester(h - 2, w + 2))
        {
            if (Random.Range(0, 8) == 0)
                roomMatrix[h, w] = edgeTiles[2];
        }
    }
    private string RandomEdgeTile()
    {
        return edgeTiles[Random.Range(0,2)];
    }

     private string RandomBackgroundTile()
    {
        return backgroundTiles[Random.Range(0,2)];
    }

    private bool EdgeTester(int h, int w)
    {
        for (int i = 0; i < edgeTiles.Length; i++)
        {
            if (roomMatrix[h, w] == edgeTiles[i])
            {
                return true;
            }
        }
        return false;
    }

    private int AddBackgroundTilesOnLeft(int wStepCounter, int column, int firstColumn, int h)
    {
        while (wStepCounter < column)
        {
            roomMatrix[h, firstColumn] = backgroundTiles[4];
            tileRotations[h, firstColumn] = 0;
            
            if (wStepCounter > firstColumn && !EdgeTester(h, wStepCounter))
            {
                roomMatrix[h, wStepCounter] = RandomBackgroundTile();
                tileRotations[h, wStepCounter] = 0;
            }
            if (wStepCounter + 1 == column && !EdgeTester(h, wStepCounter + 1))
            {
                roomMatrix[h, wStepCounter + 1] = backgroundTiles[3];
                tileRotations[h, wStepCounter + 1] = 0;
            }
            wStepCounter++;
        }

        while (wStepCounter > column)
        {
            roomMatrix[h, firstColumn] = backgroundTiles[3];
            tileRotations[h, firstColumn] = -90;

            if (wStepCounter < firstColumn && !EdgeTester(h, wStepCounter))
            {
                roomMatrix[h, wStepCounter] = RandomBackgroundTile();
                tileRotations[h, wStepCounter] = 180;
            }
            if (wStepCounter - 1 == column && !EdgeTester(h, wStepCounter - 1))
            {
                roomMatrix[h, wStepCounter - 1] = backgroundTiles[4];
                tileRotations[h, wStepCounter - 1] = -90;
            }
            wStepCounter--;
        }
        return wStepCounter;
    }

    private int AddBackgroundTilesOnRight(int wStepCounter, int column, int firstColumn, int h)
    {
        while (wStepCounter < column)
        {
            roomMatrix[h, firstColumn] = backgroundTiles[3];
            tileRotations[h, firstColumn] = 180;

            if (wStepCounter > firstColumn && !EdgeTester(h, wStepCounter))
            {
                roomMatrix[h, wStepCounter] = RandomBackgroundTile();
                tileRotations[h, wStepCounter] = 180;
            }
            if (wStepCounter + 1 == column && !EdgeTester(h, wStepCounter + 1))
            {
                roomMatrix[h, wStepCounter + 1] = backgroundTiles[4];
                tileRotations[h, wStepCounter + 1] = 180;
            }
            wStepCounter++;
        }

        while (wStepCounter > column)
        {
            roomMatrix[h, firstColumn] = backgroundTiles[4];
            tileRotations[h, firstColumn] = 90;

            if (wStepCounter < firstColumn && !EdgeTester(h, wStepCounter))
            {
                roomMatrix[h, wStepCounter] = RandomBackgroundTile();
                tileRotations[h, wStepCounter] = 0;
            }
            if (wStepCounter - 1 == column && !EdgeTester(h, wStepCounter - 1))
            {
                roomMatrix[h, wStepCounter - 1] = backgroundTiles[3];
                tileRotations[h, wStepCounter - 1] = 90;
            }
            wStepCounter--;
        }
        return wStepCounter;
    }

    private void AddBackground()
    {
        int column1;
        int column2;
        int gapSize = 4;
        
         for (int w = Random.Range(8,gapSize + 5); w < width - 3; w += Random.Range(gapSize + 3, gapSize + 8))
        {
            int hCounter1 = Random.Range(2, 5);
            int hCounter2 = Random.Range(2, 5);
            int h1 = 1;
            int h2 = 1;
            column1 = w;
            column2 = w - gapSize;

            int firstColumn1 = column1;
            int wStepCounter1 = firstColumn1;
            int firstColumn2 = column2;
            int wStepCounter2 = firstColumn2;

            if (roomMatrix[2 ,column1] != null)
                {
                    h1 = 3;
                }

            else if (roomMatrix[1 ,column1] != null)
                {
                    h1 = 2;
                }

            if (roomMatrix[2 ,column2] != null)
                {
                    h2 = 3;
                }
                
            else if (roomMatrix[1 ,column2] != null)
                {
                    h2= 2;
                }

            while (roomMatrix[h1, column1] == null)
            {
                if (h1 == hCounter1)
                {
                    firstColumn1 = column1;
                    wStepCounter1 = firstColumn1;
                    column1 = Random.Range(firstColumn1 - 1,firstColumn1 + 2);
                    hCounter1 = Random.Range(hCounter1 + 3, height - 3);
                }
                if (!EdgeTester(h1, column1))
                {
                    roomMatrix[h1, column1] = RandomBackgroundTile();
                    tileRotations[h1, column1] = -90;
                }

                wStepCounter1 = AddBackgroundTilesOnLeft(wStepCounter1, column1, firstColumn1, h1);
                h1++;
            }

            while (roomMatrix[h2, column2] == null)
            {
                if (h2 == hCounter2)
                {
                    firstColumn2 = column2;
                    wStepCounter2 = firstColumn2;
                    column2 = Random.Range(firstColumn2 - 1,firstColumn2 + 2);
                    hCounter2 = Random.Range(hCounter2 + 3, height - 3);
                }
                if (!EdgeTester(h2, column2))
                {
                    roomMatrix[h2, column2] = RandomBackgroundTile();
                    tileRotations[h2, column2] = 90;
                }

                wStepCounter2 = AddBackgroundTilesOnRight(wStepCounter2, column2, firstColumn2, h2);
                h2++;
            }
        }
    }

    private void AddTraps()
    {
        for (int w = 1; w < width - 1; w++)
        {
            if (roomMatrix[height - 3, w] == null && roomMatrix[height - 2, w + 1] == null && roomMatrix[height - 3, w - 1] != null && roomMatrix[height - 3, w + 2] != null)
            {
                roomMatrix[height - 2, w] = innerTiles[0];
                roomMatrix[height - 2, w + 1] = innerTiles[0];
            }
        }
    }
}
