using System.Collections;
using System.Collections.Generic;
using MapGenerator.Generator;
using UnityEngine;

public class HMapGenerator
{
    HTiles[,] tiles;
    
    HMapGenerator(int width, int height)
    {
        tiles = new HTiles[width, height];
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                tiles[i, j] = new HTiles();
            }
        }
    }

    void Generator()
    {
        
    }

    void GroundGenerator()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            
        }
    }
}
