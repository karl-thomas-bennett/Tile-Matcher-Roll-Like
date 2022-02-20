using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int size;
    public float tileSize;
    public float boarderWidth = 0.05f;
    private readonly int sortingMax = 3;
    public List<GameObject> tilePool;
    public GameObject square;
    public GameObject group;
    public Color borderColour;
    public Color coverColour;
    public Color highlightColour;
    private GridData data;
    private GameObject covers;
    private GameObject topCover;
    private GameObject rightCover;
    private GameObject bottomCover;
    private GameObject leftCover;
    private GameObject tiles;
    private GameObject highlights;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        tiles = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        tiles.name = "Tiles";
        highlights = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        highlights.name = "Highlights";
        data = new GridData(size, tilePool.Count, transform);
        AddCovers();
        UpdateGrid();
        time = Time.time;
    }

    public void UpdateGrid()
    {
        DestroyAllTiles();
        //Create Tiles based on GridData
        for (int i = 0; i < data.rows.Count; i++)
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

    public void DestroyAllTiles()
    {
        Destroy(tiles);
        tiles = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        tiles.name = "Tiles";
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
        boarderRenderer.sortingOrder = sortingMax;
        float coverHeight = 5 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3(0, (size * tileSize + coverHeight) / 2), Quaternion.identity, topCover.transform);
        cover.transform.localScale = new Vector3(12, coverHeight);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = sortingMax - 1;
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
        boarderRenderer.sortingOrder = sortingMax;
        float coverWidth = 11 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3((size * tileSize + coverWidth) / 2, 0), Quaternion.identity, rightCover.transform);
        cover.transform.localScale = new Vector3(coverWidth, 10);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = sortingMax - 1;
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
        boarderRenderer.sortingOrder = sortingMax;
        float coverHeight = 5 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3(0, -(size * tileSize + coverHeight)/2), Quaternion.identity, bottomCover.transform);
        cover.transform.localScale = new Vector3(12, coverHeight);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = sortingMax - 1;
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
        boarderRenderer.sortingOrder = sortingMax;
        float coverWidth = 11 - size * tileSize / 2;
        GameObject cover = Instantiate(square, new Vector3(-(size * tileSize + coverWidth) / 2, 0), Quaternion.identity, leftCover.transform);
        cover.transform.localScale = new Vector3(coverWidth, 10);
        SpriteRenderer coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.color = coverColour;
        coverRenderer.sortingOrder = sortingMax - 1;
        boarder.name = "Boarder";
        cover.name = "Cover";
    }

    public void RemoveCovers()
    {

    }



    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) / (tileSize);
        int mouseX = Mathf.RoundToInt(mouse.x);
        int mouseY = Mathf.RoundToInt(mouse.y);
        if (highlights.transform.childCount < 2)
        {
            GameObject rowHighlight = Instantiate(square, new Vector2(0, mouseY * tileSize), Quaternion.identity, highlights.transform);
            rowHighlight.name = "Row Highlight";
            SpriteRenderer renderer = rowHighlight.GetComponent<SpriteRenderer>();
            renderer.color = highlightColour;
            renderer.sortingOrder = sortingMax - 2;
            rowHighlight.transform.localScale = new Vector3(size * tileSize, tileSize, 1);
        }
        else
        {
            Transform rowTransform = highlights.transform.GetChild(0);
            rowTransform.localPosition = new Vector2(0, (mouseY) * tileSize);
        }

        if (highlights.transform.childCount < 2)
        {
            GameObject columnHighlight = Instantiate(square, new Vector2(mouseX * tileSize, 0), Quaternion.identity, highlights.transform);
            columnHighlight.name = "Column Highlight";
            SpriteRenderer renderer = columnHighlight.GetComponent<SpriteRenderer>();
            renderer.color = highlightColour;
            renderer.sortingOrder = sortingMax - 2;
            columnHighlight.transform.localScale = new Vector3(tileSize, size * tileSize, 1);
        }
        else
        {
            Transform rowTransform = highlights.transform.GetChild(1);
            rowTransform.localPosition = new Vector2(mouseX * tileSize, 0);
        }



    }
    
}
