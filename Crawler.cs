using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Maze
{
    public override void Generate()
    {
        CrawlHorizontal();
        CrawlVertical();
    }

    void CrawlHorizontal()
    {
        bool done = false;
        int x = 1;
        int z = _depth / 2;

        while (!done)
        {
            map[x, z] = 0;
            if (Random.Range(0, 100) < 50)
            {
                x += Random.Range(0, 2);
            }
            else
            {
                z += Random.Range(-1, 2);
            }
            done |= (x < 1 || x >= _width - 1 || z < 1 || z >= _depth - 1);
        }
    }
    void CrawlVertical()
    {
        bool done = false;
        int x = _width / 2;
        int z = 1;

        while (!done)
        {
            map[x, z] = 0;
            if (Random.Range(0, 100) < 50)
            {
                x += Random.Range(-1, 2);
            }
            else
            {
                z += Random.Range(0, 2);
            }
            done |= (x < 1 || x >= _width - 1 || z < 1 || z >= _depth - 1);
        }
    }
}
