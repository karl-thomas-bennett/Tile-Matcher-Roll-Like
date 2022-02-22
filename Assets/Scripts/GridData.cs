using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public List<List<int>> columns;
    public List<List<int>> rows;
    public List<List<int>> square;
    private int poolSize;
    public List<int> visibleColumnStarts;
    public List<int> visibleRowStarts;
    private int size = -1;
    private int buffer;
    private Transform parent;

    public GridData(int size, int poolSize, Transform parent, int buffer = 5)
    {
        visibleColumnStarts = new List<int>();
        visibleRowStarts = new List<int>();
        columns = new List<List<int>>();
        rows = new List<List<int>>();
        square = new List<List<int>>();
        this.poolSize = poolSize;
        this.buffer = buffer;
        SetGridSize(size);
        GenerateGrid();
        RemoveMatches();
    }

    public void UpdateGrid(List<List<int>> rows, List<List<int>> columns, List<int> visibleRowStarts, List<int> visibleColumnStarts)
    {
        this.rows = rows;
        this.columns = columns;
        this.visibleRowStarts = visibleRowStarts;
        this.visibleColumnStarts = visibleColumnStarts;
        //Update square
        //Clear matches
        //Move tiles down into gaps
        //Generate new tiles
        
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

    public void RemoveMatches()
    {
        List<Match> matches = FindMatches();
        foreach (Match match in matches)
        {
            foreach (Vector2 tile in match.tiles)
            {
                SetTile((int)tile.x,(int)tile.y, NotNeighbour(tile));
            }
        }
    }

    public List<Vector2> GetNeighbours(Vector2 tile)
    {
        List<Vector2> neighbours = new List<Vector2>();
        if(tile.y + 1 < size)
        {
            neighbours.Add(new Vector2(tile.x, tile.y + 1));
        }
        if (tile.y - 1 >= 0)
        {
            neighbours.Add(new Vector2(tile.x, tile.y - 1));
        }
        if (tile.x + 1 < size)
        {
            neighbours.Add(new Vector2(tile.x + 1, tile.y));
        }
        if (tile.x - 1 >= 0)
        {
            neighbours.Add(new Vector2(tile.x - 1, tile.y));
        }
        return neighbours;
    }

    public int NotNeighbour(Vector2 tile)
    {
        List<Vector2> neighbours = GetNeighbours(tile);
        List<int> tileTypes = new List<int>();
        for(int i = 0; i < poolSize; i++)
        {
            tileTypes.Add(i);
        }
        foreach(Vector2 n in neighbours)
        {
            tileTypes.Remove(square[(int)n.x][(int)n.y]);
        }
        return tileTypes[0];
    }

    public List<Match> FindMatches()
    {
        List<Match> matches = new List<Match>();
        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                bool verticalMatchFound = false;
                bool horizontalMatchFound = false;
                for (int k = 0; k < matches.Count; k++)
                {
                    if (matches[k].tiles.Contains(new Vector2(i, j)))
                    {
                        if (matches[k].vertical)
                        {
                            verticalMatchFound = true;
                        }
                        else
                        {
                            horizontalMatchFound = true;
                        }
                    }
                }
                //Debug.Log(verticalMatchFound + " " + horizontalMatchFound);
                if (!verticalMatchFound)
                {
                    List<Vector2> tiles = new List<Vector2>();
                    int l = j;
                    Vector2 current = new Vector2(i, l);
                    tiles.Add(current);
                    l++;
                    current = new Vector2(i, l);
                    while (current.y < square.Count && square[(int)current.x][(int)current.y] == square[i][j])
                    {
                        tiles.Add(current);
                        l++;
                        current = new Vector2(i, l);
                    }
                    l = j - 1;
                    current = new Vector2(i, l);
                    while (current.y >= 0 && square[(int)current.x][(int)current.y] == square[i][j])
                    {
                        tiles.Add(current);
                        l--;
                        current = new Vector2(i, l);
                    }
                    if(tiles.Count > 2)
                    {
                        matches.Add(new Match(tiles, square[(int)tiles[0].x][(int)tiles[0].y]));
                    }
                        
                }
                if (!horizontalMatchFound)
                {
                    List<Vector2> tiles = new List<Vector2>();
                    int l = i;
                    Vector2 current = new Vector2(l, j);
                    tiles.Add(current);
                    l++;
                    current = new Vector2(l, j);
                    while (current.x < square.Count && square[(int)current.x][(int)current.y] == square[i][j])
                    {
                        tiles.Add(current);
                        l++;
                        current = new Vector2(l, j);
                    }
                    l = i - 1;
                    current = new Vector2(l, j);
                    while (current.x >= 0 && square[(int)current.x][(int)current.y] == square[i][j])
                    {
                        tiles.Add(current);
                        l--;
                        current = new Vector2(l, j);
                    }
                    if (tiles.Count > 2)
                    {
                        matches.Add(new Match(tiles, square[(int)tiles[0].x][(int)tiles[0].y]));
                    }
                }
            }
        }
        return matches;
    }

    public void SetTile(int r, int c, int value)
    {
        square[r][c] = value;
        rows[r][c + visibleRowStarts[r]] = value;
        columns[c][r + visibleColumnStarts[c]] = value;
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
            square.Add(new List<int>());
            //Adding tiles to all rows
            for (int j = 0; j < size + 10; j++)
            {
                int tile = GenerateTile();
                rows[i].Add(tile);
                //Adding the same tile to columns when there is overlap
                if (j >= buffer && j < buffer + size)
                {
                    columns[j - buffer].Add(tile);
                    square[i].Add(tile);
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
    public override string ToString()
    {
        string output = "";
        for(int i = size - 1; i >= 0; i--)
        {
            for(int j = 0; j < size; j++)
            {
                output += square[i][j];
            }
            output += "\n";
        }
        return output;
    }
}
