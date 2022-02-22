using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int size;
    public float tileSize;
    public int buffer = 5;
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
    private Vector2 originalMousePosition;
    private string directionLock = "";
    private List<List<GameObject>> rows;
    private List<List<GameObject>> columns;
    // Start is called before the first frame update
    void Start()
    {
        tiles = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        tiles.name = "Tiles";
        highlights = Instantiate(group, Vector3.zero, Quaternion.identity, transform);
        highlights.name = "Highlights";
        data = new GridData(size, tilePool.Count, transform);
        rows = new List<List<GameObject>>();
        columns = new List<List<GameObject>>();
        AddCovers();
        CreateGrid();
        time = Time.time;
    }

    public void CreateGrid()
    {
        DestroyAllTiles();
        //Create Tiles based on GridData
        for (int i = 0; i < data.columns.Count; i++)
        {
            columns.Add(new List<GameObject>());
        }
        for (int i = 0; i < data.rows.Count; i++)
        {
            rows.Add(new List<GameObject>());
            for(int j = 0; j < data.rows[i].Count; j++)
            {
                GameObject tile = Instantiate(tilePool[data.rows[i][j]], new Vector3(tileSize*(j - buffer + 0.5f - size/2f), tileSize*(i + 0.5f - size/2f)), Quaternion.identity, tiles.transform);
                tile.transform.localScale = new Vector3(tileSize, tileSize);
                tile.AddComponent<Tile>();
                Tile t = tile.GetComponent<Tile>();
                t.r = i;
                t.c = j - 5;
                t.gridSize = size;
                t.tileSize = tileSize;
                t.tile = data.rows[i][j];
                t.buffer = buffer;
                rows[i].Add(tile);
                if(j >= buffer && j < buffer + size)
                {
                    columns[j - buffer].Add(tile);
                }
            }
        }
        for (int i = 0; i < data.columns.Count; i++)
        {
            for (int j = 0; j < data.columns[i].Count; j++)
            {
                if(j < buffer || j >= buffer + size)
                {
                    GameObject tile = Instantiate(tilePool[data.columns[i][j]], new Vector3(tileSize*(i + 0.5f - size/2f), tileSize*(j - buffer + 0.5f - size/2f)), Quaternion.identity, tiles.transform);
                    tile.transform.localScale = new Vector3(tileSize, tileSize);
                    tile.AddComponent<Tile>();
                    Tile t = tile.GetComponent<Tile>();
                    t.r = j - 5;
                    t.c = i;
                    t.gridSize = size;
                    t.tileSize = tileSize;
                    t.tile = data.columns[i][j];
                    t.buffer = buffer;
                    if (j < buffer)
                    {
                        columns[i].Insert(j, tile);
                    }
                    else
                    {
                        columns[i].Add(tile);
                    }
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

    private void RowHighlight(int y)
    {
        if (highlights.transform.childCount < 2)
        {
            GameObject rowHighlight = Instantiate(square, new Vector2(0, (y + 0.5f*(1-size%2)) * tileSize), Quaternion.identity, highlights.transform);
            rowHighlight.name = "Row Highlight";
            SpriteRenderer renderer = rowHighlight.GetComponent<SpriteRenderer>();
            renderer.color = highlightColour;
            renderer.sortingOrder = sortingMax - 2;
            rowHighlight.transform.localScale = new Vector3(size * tileSize, tileSize, 1);
        }
        else
        {
            Transform rowTransform = highlights.transform.GetChild(0);
            rowTransform.localPosition = new Vector2(0, (y + 0.5f * (1 - size % 2)) * tileSize);
        }
    }

    private void ColumnHighlight(int x)
    {
        if (highlights.transform.childCount < 2)
        {
            GameObject columnHighlight = Instantiate(square, new Vector2((x + 0.5f * (1 - size % 2)) * tileSize, 0), Quaternion.identity, highlights.transform);
            columnHighlight.name = "Column Highlight";
            SpriteRenderer renderer = columnHighlight.GetComponent<SpriteRenderer>();
            renderer.color = highlightColour;
            renderer.sortingOrder = sortingMax - 2;
            columnHighlight.transform.localScale = new Vector3(tileSize, size * tileSize, 1);
        }
        else
        {
            Transform rowTransform = highlights.transform.GetChild(1);
            rowTransform.localPosition = new Vector2((x + 0.5f * (1 - size % 2)) * tileSize, 0);
        }
    }



    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) / (tileSize);
        int mouseX = size % 2 == 0 ? Mathf.FloorToInt(mouse.x) : Mathf.RoundToInt(mouse.x);
        int mouseY = size % 2 == 0 ? Mathf.FloorToInt(mouse.y) : Mathf.RoundToInt(mouse.y);
        RowHighlight(mouseY);
        ColumnHighlight(mouseX);
        if (Input.GetMouseButtonUp(0))
        {
            if (directionLock.Equals("Horizontal"))
            {
                mouseY = size % 2 == 0 ? Mathf.FloorToInt(originalMousePosition.y) : Mathf.RoundToInt(originalMousePosition.y);
                bool inverse = Mathf.Sign(originalMousePosition.x - mouse.x) > 0;
                int rowIndex = mouseY + size / 2;
                for (int i = inverse ? rows[rowIndex].Count - 1 : 0; inverse ? i >= 0 : i < rows[rowIndex].Count; i += inverse? -1: 1) 
                {
                    Tile t = rows[rowIndex][i].GetComponent<Tile>();
                    t.FinalisePosition("Horizontal", rows, columns, data.visibleRowStarts, data.visibleColumnStarts);
                }
                //TODO: Update data
            }
            if (directionLock.Equals("Vertical"))
            {
                mouseX = size % 2 == 0 ? Mathf.FloorToInt(originalMousePosition.x) : Mathf.RoundToInt(originalMousePosition.x);
                bool inverse = Mathf.Sign(originalMousePosition.y - mouse.y) > 0;
                int colIndex = mouseX + size / 2;
                for (int i = inverse ? columns[colIndex].Count - 1 : 0; inverse ? i >= 0 : i < columns[colIndex].Count; i += inverse ? -1 : 1)
                {
                    Tile t = columns[colIndex][i].GetComponent<Tile>();
                    t.FinalisePosition("Vertical", rows, columns, data.visibleRowStarts, data.visibleColumnStarts);
                }
                //TODO: Update data
            }
            directionLock = "";
            originalMousePosition = new Vector2(1000, 1000);
        }
        if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
        {
            float deltaX = mouse.x - originalMousePosition.x;
            float deltaY = mouse.y - originalMousePosition.y;
            if (directionLock == "")
            {
                if (Mathf.Abs(deltaX) > 1 || Mathf.Abs(deltaY) > 1)
                {
                    if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
                    {
                        directionLock = "Horizontal";
                    }
                    else
                    {
                        directionLock = "Vertical";
                    }
                }
                
            }
            if (directionLock.Equals("Horizontal"))
            {
                mouseX = size % 2 == 0 ? Mathf.FloorToInt(originalMousePosition.x) : Mathf.RoundToInt(originalMousePosition.x);
                mouseY = size % 2 == 0 ? Mathf.FloorToInt(originalMousePosition.y) : Mathf.RoundToInt(originalMousePosition.y);
                foreach(GameObject g in rows[mouseY + size / 2])
                {
                    Tile t = g.GetComponent<Tile>();
                    t.Move(new Vector2(deltaX, 0));
                }
            }
            if (directionLock.Equals("Vertical"))
            {
                mouseX = size % 2 == 0 ? Mathf.FloorToInt(originalMousePosition.x) : Mathf.RoundToInt(originalMousePosition.x);
                mouseY = size % 2 == 0 ? Mathf.FloorToInt(originalMousePosition.y) : Mathf.RoundToInt(originalMousePosition.y);
                foreach (GameObject g in columns[mouseX + size / 2])
                {
                    Tile t = g.GetComponent<Tile>();
                    t.Move(new Vector2(0, deltaY));
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            originalMousePosition = new Vector2(mouse.x, mouse.y);
        }


    }
    
}
