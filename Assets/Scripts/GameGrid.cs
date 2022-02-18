using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int size;
    public float tileSize;
    public float boarderWidth = 0.05f;
    public List<GameObject> tilePool;
    public GameObject square;
    public GameObject group;
    public Color borderColour;
    public Color coverColour;
    private GridData data;
    private GameObject covers;
    private GameObject topCover;
    private GameObject rightCover;
    private GameObject bottomCover;
    private GameObject leftCover;
    private GameObject tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        tiles.name = "Tiles";
        data = new GridData(size, tilePool.Count, transform);
        AddCovers();
        UpdateGrid();
        
    }

    public void UpdateGrid()
    {
        //Create Tiles based on GridData
        for(int i = 0; i < data.rows.Count; i++)
        {
            for(int j = 0; j < data.rows[i].Count; j++)
            {
                GameObject tile = Instantiate(tilePool[data.rows[i][j]], new Vector3(tileSize*(j - 4.5f - size/2f), tileSize*(i + 0.5f - size/2f)), Quaternion.identity, tiles.transform);
                tile.transform.localScale = new Vector3(tileSize, tileSize);
            }
        }
        for (int i = 0; i < data.columns.Count; i++)
        {
            for (int j = 0; j < data.columns[i].Count; j++)
            {
                if(j < 5 || j >= 5 + size)
                {
                    GameObject tile = Instantiate(tilePool[data.columns[i][j]], new Vector3(tileSize*(i + 0.5f - size/2f), tileSize*(j-4.5f - size/2f)), Quaternion.identity, tiles.transform);
                    tile.transform.localScale = new Vector3(tileSize, tileSize);
                }
            }
        }
    }

    public void AddCovers()
    {
        covers = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        covers.name = "Covers";
        AddTopCover();
        AddRightCover();
        AddBottomCover();
        AddLeftCover();
    }

    public void AddTopCover()
    {
        topCover = Instantiate(group, new Vector3(), Quaternion.identity, covers.transform);
        topCover.name = "TopCover";
        GameObject boarder = Instantiate(square, new Vector3(0, size * tileSize / 2), Quaternion.identity, topCover.transform);
        boarder.transform.localScale = new Vector3(size * tileSize + boarderWidth * 2, boarderWidth);
        SpriteRenderer boarderRenderer = boarder.GetComponent<SpriteRenderer>();
        boarderRenderer.color = borderColour;
        boarderRenderer.sortingOrder = 2;
        float coverHeight = 5 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3(0, (size * tileSize + coverHeight) / 2), Quaternion.identity, topCover.transform);
        cover.transform.localScale = new Vector3(12, coverHeight);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = 1;
        boarder.name = "Boarder";
        cover.name = "Cover";
    }
    public void AddRightCover()
    {
        rightCover = Instantiate(group, new Vector3(), Quaternion.identity, covers.transform);
        rightCover.name = "RightCover";
        GameObject boarder = Instantiate(square, new Vector3(size * tileSize / 2, 0), Quaternion.identity, rightCover.transform);
        boarder.transform.localScale = new Vector3(boarderWidth, size * tileSize + boarderWidth * 2);
        SpriteRenderer boarderRenderer = boarder.GetComponent<SpriteRenderer>();
        boarderRenderer.color = borderColour;
        boarderRenderer.sortingOrder = 2;
        float coverWidth = 11 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3((size * tileSize + coverWidth) / 2, 0), Quaternion.identity, rightCover.transform);
        cover.transform.localScale = new Vector3(coverWidth, 10);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = 1;
        boarder.name = "Boarder";
        cover.name = "Cover";
    }
    public void AddBottomCover()
    {
        bottomCover = Instantiate(group, new Vector3(), Quaternion.identity, covers.transform);
        bottomCover.name = "BottomCover";
        GameObject boarder = Instantiate(square, new Vector3(0, -size * tileSize / 2), Quaternion.identity, bottomCover.transform);
        boarder.transform.localScale = new Vector3(size * tileSize + boarderWidth * 2, boarderWidth);
        SpriteRenderer boarderRenderer = boarder.GetComponent<SpriteRenderer>();
        boarderRenderer.color = borderColour;
        boarderRenderer.sortingOrder = 2;
        float coverHeight = 5 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3(0, -(size * tileSize + coverHeight)/2), Quaternion.identity, bottomCover.transform);
        cover.transform.localScale = new Vector3(12, coverHeight);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = 1;
        boarder.name = "Boarder";
        cover.name = "Cover";
    }
    public void AddLeftCover()
    {
        leftCover = Instantiate(group, new Vector3(), Quaternion.identity, covers.transform);
        leftCover.name = "RightCover";
        GameObject boarder = Instantiate(square, new Vector3(-size * tileSize / 2, 0), Quaternion.identity, leftCover.transform);
        boarder.transform.localScale = new Vector3(boarderWidth, size * tileSize + boarderWidth * 2);
        SpriteRenderer boarderRenderer = boarder.GetComponent<SpriteRenderer>();
        boarderRenderer.color = borderColour;
        boarderRenderer.sortingOrder = 2;
        float coverWidth = 11 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3(-(size * tileSize + coverWidth) / 2, 0), Quaternion.identity, leftCover.transform);
        cover.transform.localScale = new Vector3(coverWidth, 10);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = 1;
        boarder.name = "Boarder";
        cover.name = "Cover";
    }

    public void RemoveCovers()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
