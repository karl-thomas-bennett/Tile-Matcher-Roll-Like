using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private List<List<GameObject>> columns;
    private List<List<GameObject>> rows;
    private List<int> visibleColumnStarts;
    private List<int> visibleRowStarts;
    private int size = -1;
    private int buffer;

    public GridData(int size, int buffer = 5)
    {
        this.buffer = buffer;
        SetGridSize(size);
        GenerateGrid();
    }

    

    public void MoveColumn(int columnNumber, int amount)
    {
    }

    public GameObject GenerateTile()
    {
        return null;
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
            rows.Add(new List<GameObject>());
            columns.Add(new List<GameObject>());
            visibleRowStarts.Add(buffer);
            visibleColumnStarts.Add(buffer);
        }
        for (int i = 0; i < size; i++)
        {
            //Adding tiles to all rows
            for (int j = 0; j < size + 10; j++)
            {
                GameObject tile = GenerateTile();
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
                GameObject tile = GenerateTile();
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
