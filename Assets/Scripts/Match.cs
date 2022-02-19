using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    public List<Vector2> tiles;
    public int tile;
    public int Length;
    public bool vertical;

    public Match(List<Vector2> tiles, int tile)
    {
        this.tiles = tiles;
        this.tile = tile;
        Length = tiles.Count;
        vertical = tiles[0].y != tiles[1].y;
    }
}
