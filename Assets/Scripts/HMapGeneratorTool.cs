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
    
    //Points
    public int waterPoint = 1;


    public GameObject prefab;
    public HTileList tileList; //UI매니저가 생기면 사라질것임
    private HTiles[,] tiles;

    void Start()
    {
        prefab = Resources.Load("Prefabs/Tile") as GameObject;
        
        GenerateMap();
        GenerateWaterMap();
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
                tiles[x, y].terrainType = TerrainType.Ground;
                
                //SOTile 할당
                tiles[x, y].tileList = this.tileList;
                tiles[x, y].cellSize = cellSize;
            }
        }
        
    }

    #region WaterGenerate
    void GenerateWaterMap()
    {
        //랜덤한값을 찍어서 주위 4칸이 양수면 물이되는 재귀함수
        //그리고 워터포인트만큼 함수 반복
        int waterPointLimit = 10;
        while (waterPoint > 0)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (WaterExpansion(x, y))
            {
                waterPoint--;
                waterPointLimit = 10;
            }
            
            if (waterPointLimit-- < 0) break;
        }
    }

    public bool WaterExpansion(int x,int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return false;
        
        if (tiles[x, y].noiseValue > 0 && tiles[x, y].terrainType != TerrainType.Water)
        {
            tiles[x, y].terrainType = TerrainType.Water;
            WaterExpansion(x + 1, y);
            WaterExpansion(x - 1, y);
            WaterExpansion(x, y + 1);
            WaterExpansion(x, y - 1);
            return true;
        }
        return false;
    }
    #endregion
}
