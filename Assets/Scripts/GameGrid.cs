using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int size;
    public float tileSize;
    public List<GameObject> tilePool;
    private GridData data;
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
                Instantiate(tilePool[data.rows[i][j]], new Vector3(-5 + j, i), Quaternion.identity, transform);
            }
        }
        for (int i = 0; i < data.columns.Count; i++)
        {
            for (int j = 0; j < data.columns[i].Count; j++)
            {
                if(j < 5 || j >= 5 + size)
                {
                    Instantiate(tilePool[data.columns[i][j]], new Vector3(i, j-5), Quaternion.identity, transform);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
