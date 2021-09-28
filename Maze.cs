﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}
public class Maze : MonoBehaviour
{
    [SerializeField]
    public int _width, _depth;
    public byte[,] map;
    public int scale = 6;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseMap();
        Generate();
        DrawMap();
        
    }

    void InitialiseMap()
    {
        map = new byte[_width, _depth];
        for (int z = 0; z < _depth; z++)
            for (int x = 0; x < _width; x++)
            {
                    map[x, z] = 1; // 1 = wall 0 = corridor     
            }
    }
    public virtual void Generate()
    {
        for (int z = 0; z < _depth; z++)
            for (int x = 0; x < _width; x++)
            {
                if(Random.Range(0, 100) < 50)
                {
                    map[x, z] = 0; // 1 = wall 0 = corridor   
                }
                 
            }
    }
    void DrawMap()
    {
        for (int z = 0; z < _depth; z++)
            for (int x = 0; x < _width; x++)
            {
                if (map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    wall.transform.position = pos;
                }
                
            }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= _width - 1 || z <= 0 || z >= _depth - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        if (map[x, z + 1] == 0) count++;

        return count;
    }
    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= _width - 1 || z <= 0 || z >= _depth - 1) return 5;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z + 1] == 0) count++;
        if (map[x - 1, z + 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;
        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeighbours(x,z) + CountDiagonalNeighbours(x,z);
    }
}
