using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int size;
    public float tileSize;
    public List<GameObject> tilePool;
    public GameObject square;
    public GameObject group;
    public Color borderColour;
    public Color coverColour;
    private GridData data;
    private GameObject topCover;
    private GameObject rightCover;
    private GameObject bottomCover;
    private GameObject leftCover;
    // Start is called before the first frame update
    void Start()
    {
        data = new GridData(size, tilePool.Count, transform);
        UpdateGrid();
    }

    public void UpdateGrid()
    {
        //Create Tiles based on GridData
        for(int i = 0; i < data.rows.Count; i++)
        {
            for(int j = 0; j < data.rows[i].Count; j++)
            {
                GameObject tile = Instantiate(tilePool[data.rows[i][j]], new Vector3(tileSize*(j - 4.5f - size/2), tileSize*(i + 0.5f - size/2)), Quaternion.identity, transform);
                tile.transform.localScale = new Vector3(tileSize, tileSize);
            }
        }
        for (int i = 0; i < data.columns.Count; i++)
        {
            for (int j = 0; j < data.columns[i].Count; j++)
            {
                if(j < 5 || j >= 5 + size)
                {
                    GameObject tile = Instantiate(tilePool[data.columns[i][j]], new Vector3(tileSize*(i + 0.5f- size/2), tileSize*(j-4.5f - size/2)), Quaternion.identity, transform);
                    tile.transform.localScale = new Vector3(tileSize, tileSize);
                }
            }
        }
    }

    public void AddCovers()
    {
        topCover = Instantiate(group, transform);
        GameObject boarder = Instantiate(square, new Vector3(), Quaternion.identity, topCover.transform);
        boarder.transform.localScale = new Vector3(size*tileSize, 0.05f);
        boarder.GetComponent<SpriteRenderer>().color = borderColour;
        GameObject cover = Instantiate(square, new Vector3(0, 5), Quaternion.identity, topCover.transform);
        cover.transform.localScale = new Vector3();
        cover.GetComponent<SpriteRenderer>().color = coverColour;
    }

    public void RemoveCovers()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
