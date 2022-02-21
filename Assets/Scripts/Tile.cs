using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int r;
    public int c;
    public int gridSize;
    public int tile;
    public float tileSize;
    public int buffer;
    private Vector2 position;

    private void Start()
    {
        position = new Vector2(transform.position.x, transform.position.y);
    }

    public void Move(Vector2 pos)
    {
        if(pos.magnitude < buffer * tileSize)
        {
            transform.position = position + pos;
        }
        else
        {
            transform.position = position + pos.normalized*buffer*tileSize;
        }
        
    }

    public void FinalisePosition(string directionLock, List<List<GameObject>> rows, List<List<GameObject>> columns)
    {
        if (directionLock.Equals("Horizontal"))
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x / tileSize) * tileSize, transform.position.y, 0);
            int index = (int)(transform.position.x / tileSize);

            
        }
        if (directionLock.Equals("Vertical"))
        {
            transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y / tileSize) * tileSize, 0);
        }
        position = new Vector2(transform.position.x, transform.position.y);

    }
}
