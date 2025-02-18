using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMapGeneratorTool : MonoBehaviour
{
    
    //Grid
    public int height = 10;
    public int width = 10;
    public int cellSize = 1;
    public float scale = 20;
    
    //PerlinNoise
    public int seed = 1234;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 0.5f;
    
    
    public GameObject prefab;
    private HTiles[,] tiles;

    void Start()
    {
        prefab = Resources.Load("Prefabs/Tile") as GameObject;
        
        GenerateMap();
    }


    void Update()
    {
        
    }
    
    void GenerateMap()
    {
        tiles = new HTiles[height, width];
        Camera.main.transform.position = new Vector3(width *.5f, height * .5f, -15);

        PerlinNoise noise = new PerlinNoise(seed);
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject go = Instantiate(prefab,transform.position,Quaternion.identity);
                go.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
                go.transform.position = new Vector3(x * cellSize, y * cellSize, 0) + new Vector3(cellSize * .5f, cellSize * .5f, 0);
                
                tiles[x, y] = go.AddComponent<HTiles>();
                tiles[x, y].noiseValue = noise.OctavePerlin((float)x/scale, (float)y/scale, octaves, persistence,lacunarity);
                
                Renderer renderer = go.GetComponent<Renderer>();
                renderer.material.color = Color.Lerp(Color.black, Color.white, tiles[x, y].noiseValue);
            }
        }
        
        
    }
}
