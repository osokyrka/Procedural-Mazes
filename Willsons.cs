using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willsons : Maze
{
    List<MapLocation> directions = new List<MapLocation>()
    {new MapLocation(1,0),
     new MapLocation(0,1),
     new MapLocation(-1,0),
     new MapLocation(0,-1)};

    List<MapLocation> notUsed = new List<MapLocation>();
    public override void Generate()
    {
        int x = Random.Range(2, _width - 1);
        int z = Random.Range(2, _depth - 1);
        map[x, z] = 2;

        while(GetAvailableCells() > 1)
        {
            RandomWalk();
        }
        
    }

    int CountSquareMazeNeighbours (int x, int z )
    {
        int count = 0;
        for(int d = 0; d < directions.Count; d++)
        {
            int nX = x + directions[d].x;
            int nZ = z + directions[d].z;
            if(map [nX, nZ] == 2)
            {
                count++;
            }
        }
        return count;
    }
    int GetAvailableCells()
    {
        notUsed.Clear();
        for (int z = 1; z < _depth - 1; z++)
            for (int x = 1; x < _width - 1; x++)
            {
                if (CountSquareMazeNeighbours(x, z) == 0)
                {
                    notUsed.Add(new MapLocation(x, z));
                }
            }
        return notUsed.Count;
    }
    private void RandomWalk()
    {
        List<MapLocation> inWalk = new List<MapLocation>();
        int cX;
        int cZ;
        int randomStartIndex = Random.Range(0, notUsed.Count);

        cX = notUsed[randomStartIndex].x;
        cZ = notUsed[randomStartIndex].z;
        inWalk.Add(new MapLocation(cX, cZ));

        int countLoops = 0;
        bool validPath = false;
        while (cX > 0 && cX < _width - 1 && cZ > 0 && cZ < _depth - 1 && countLoops < 5000 && !validPath)
        {
            map[cX, cZ] = 0;
            if(CountSquareMazeNeighbours(cX,cZ) > 1)
            {
                break;
            }

            int rD = Random.Range(0, directions.Count);
            int nX = cX + directions[rD].x;
            int nZ = cZ + directions[rD].z;
            if(CountSquareNeighbours(nX, nZ) < 2)
            {
                cX = nX;
                cZ = nZ;
                inWalk.Add(new MapLocation(cX, cZ));
            }
            validPath = CountSquareMazeNeighbours(cX, cZ) == 1;
            countLoops++;
        }
        if (validPath)
        {
            map[cX, cZ] = 0;
            Debug.Log("Path Found");

            foreach (MapLocation m in inWalk)
            {
                map[m.x, m.z] = 2;
            }
            inWalk.Clear();
        }
        else
        {
            foreach (MapLocation m in inWalk)
            {
                map[m.x, m.z] = 1;
                inWalk.Clear();
            }
        }
    }
}
