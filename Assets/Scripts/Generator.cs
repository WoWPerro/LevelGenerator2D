using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject white;
    private List<GameObject> objs;
    public int SizeX = 0;
    public int SizeY = 0;
    bool[,] map;
    bool[,] map2;
    public int WallCount = 0;
    public int EmptyCount = 0;
    public int iterations;

    public void CrateBoard(int _sizeX, int _sizeY, int _iterations, int _WC, int _EC)
    {
        SizeX = _sizeX;
        SizeY = _sizeY;
        iterations = _iterations;
        WallCount = _WC;
        EmptyCount = _EC;
        Clean();
        map = new bool[SizeX, SizeY];
        map2 = new bool[SizeX, SizeY];
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                if (UnityEngine.Random.Range(0f, 1f) > .5f)
                {
                    map[i, j] = true;
                }
            }
        }

        for (int i = 0; i < SizeX; i++)
        {
            map[i, 0] = true;
            map[i, SizeY - 1] = true;
            map2[i, 0] = true;
            map2[i, SizeY - 1] = true;
        }

        for (int j = 0; j < SizeY; j++)
        {
            map[0, j] = true;
            map[SizeX - 1, j] = true;
            map2[0, j] = true;
            map2[SizeX - 1, j] = true;
        }

        CheckAll();

        if (iterations > 1)
        {
            for(int i = 0; i < iterations; i++)
            {
                map = map2;
                CheckAll();
            }   
        }
        InstantiateBoard();
    }

    private void CheckAll()
    {
        for (int i = 1; i < SizeX - 1; i++)
        {
            for (int j = 1; j < SizeY - 1; j++)
            {
                Check(i, j);
            }
        }
    }

    void InstantiateBoard()
    {
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                GameObject g = Instantiate(white, new Vector2(i, -j), Quaternion.identity);
                objs.Add(g);
                g.SetActive(true);
                if (map2[i, j])
                {
                    g.GetComponent<SpriteRenderer>().color = Color.black;
                }
            }
        }
    }

    void Check(int x, int y)
    {
        int neighbors = 0;
        
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (map[x + i, y + j])
                {
                    neighbors++;
                    if (i == 0 && j == 0)
                    {
                        neighbors--;
                    }
                }
            }
        }


        if (map[x,y])
        {
            if(neighbors >= WallCount)
            {
                map2[x, y] = true;
            }
            else if(neighbors < WallCount)
            {
                map2[x, y] = false;
            }
        }
        else
        {
            if (neighbors >= EmptyCount)
            {
                map2[x, y] = true;
            }
            else if (neighbors < EmptyCount)
            {
                map2[x, y] = false;
            }
        }
    }

    void Start()
    {
        objs = new List<GameObject>();
    }

    void Clean()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            Destroy(objs[i]);
        }
        objs.Clear();
    }
}