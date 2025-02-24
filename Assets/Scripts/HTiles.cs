using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTiles : MonoBehaviour
{
    [SerializeField]private HTerrainType _hTerrainType;

    public HTerrainType hTerrainType
    {
        get { return _hTerrainType; }
        set
        {
            _hTerrainType = value;
            Renderer renderer = gameObject.GetComponent<Renderer>();
            
            switch (_hTerrainType)
            {
                case HTerrainType.Ground:
                    //Test Renderer
                    renderer.material.color = Color.white;
                    break;
                case HTerrainType.Water:
                    renderer.material.color = Color.blue;
                    break;
            }
        }
    }
    
    public SOTile tile;
    //float값으로 찾고 반환함 (HTiles로 찾게되면 명확하지 않음)
    public Dictionary<float,HTiles> aroundTiles = new Dictionary<float,HTiles>(); //(노드역할)

    public GameObject hTileObject; // 타일위의 도로혹은 건물오브젝트(아무런 컴포넌트도 없음)
    [HideInInspector] public float noiseValue;
    [HideInInspector] public float cellSize = 1;
    
    //범위체크 : 설치 가능한 범위 안에 들어오면 canSet이 트루가 됨
    public bool inSetRange = false;

    private void Start()
    {
        //HTileObject의 위치를 설정해주어야함
    }
    
    public void SetTile(SOTile tile)
    {
        this.tile = tile;
        hTileObject = tile.hTileObject;
    }

    public void DestroyTile(HTiles hTiles)
    {
        DisConnectTileAll(hTiles);
        aroundTiles.Clear();
        //현재타일 None상태하기
        SetTile(null);
    }

    public void ConnectTIle(HTiles hTiles)
    {
        if (!aroundTiles.ContainsKey(hTiles.noiseValue))
        {
            aroundTiles.Add(hTiles.noiseValue, hTiles);

            //나중에 도로끼리 이어주는 역할도 해야함
        }
    }

    public void DisConncetTile(HTiles hTiles)
    {
        if (aroundTiles.ContainsKey(hTiles.noiseValue))
        {
            aroundTiles.Remove(hTiles.noiseValue);

            //연결돼있던 도로를 끊어주어야함
        }
    }

    public void DisConnectTileAll(HTiles hTiles)
    {
        //현재 타일의 연결된 모든 노드 타일을 끊어줌
        foreach (HTiles hTile in hTiles.aroundTiles.Values)
        {
            hTile.DisConncetTile(hTiles);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , cellSize* .2f);
    }
}
