using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public List<List<int>> columns;
    public List<List<int>> rows;
    private int poolSize;
    private List<int> visibleColumnStarts;
    private List<int> visibleRowStarts;
    private int size = -1;
    private int buffer;
    private Transform parent;

    public GridData(int size, int poolSize, Transform parent, int buffer = 5)
    {
        visibleColumnStarts = new List<int>();
        visibleRowStarts = new List<int>();
        columns = new List<List<int>>();
        rows = new List<List<int>>();
        this.poolSize = poolSize;
        this.buffer = buffer;
        SetGridSize(size);
        GenerateGrid();
    }


    public void MoveColumn(int columnNumber, int amount)
    {
    }

    public int GenerateTile()
    {
        return RandomInt(0, poolSize);
    }

    private int RandomInt(int min, int max)
    {
        int output = Random.Range(min, max);
        while(output == max)
        {
            output = Random.Range(min, max);
        }
        return output;
    }

    public void ResolveMatches()
    {

    }

    public void SetGridSize(int size)
    {
        if(size != -1)
        {
            for (int i = 0; i < visibleColumnStarts.Count; i++)
            {
                visibleColumnStarts[i] += this.size - size;
                visibleRowStarts[i] += this.size - size;
            }
        }
        this.size = size;
        
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < size; i++)
        {
            rows.Add(new List<int>());
            columns.Add(new List<int>());
            visibleRowStarts.Add(buffer);
            visibleColumnStarts.Add(buffer);
        }
        for (int i = 0; i < size; i++)
        {
            //Adding tiles to all rows
            for (int j = 0; j < size + 10; j++)
            {
                int tile = GenerateTile();
                rows[i].Add(tile);
                //Adding the same tile to columns when there is overlap
                if (j >= buffer && j < buffer + size)
                {
                    columns[j - buffer].Add(tile);
                }

            }
            //Adding all tiles to columns that do not overlap with rows
            for (int j = 0; j < size + 10; j++)
            {
                int tile = GenerateTile();
                if(j < buffer)
                {
                    columns[i].Insert(0, tile);
                }
                if(j >= buffer + size)
                {
                    columns[i].Add(tile);
                }
            }
        }
    }
}
