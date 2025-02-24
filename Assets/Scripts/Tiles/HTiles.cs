using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //현재타일이 아닌 이어지는 타일의 노이즈밸류를 키값으로 넣어야함
    public Dictionary<float, LineRenderer> lineRenderers = new Dictionary<float, LineRenderer>();

    public GameObject hTileObject; // 타일위의 도로혹은 건물오브젝트(아무런 컴포넌트도 없음)
     public float noiseValue;
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
        hTileObject = tile?.hTileObject;
    }

    public void DestroyTile(HTiles hTiles)
    {
        DisConnectTileAll(hTiles);
        aroundTiles.Clear();
        SetTile(null);
    }

    public void ConnectTIle(HTiles hTiles)
    {
        if (!aroundTiles.ContainsKey(hTiles.noiseValue))
        {
            aroundTiles.Add(hTiles.noiseValue, hTiles);
            
            //이어지려는 타일에 라인렌더링이 안되어 있으면 현재 타일에서 라인렌더러를 생성 및 관리
            if (!hTiles.lineRenderers.ContainsKey(noiseValue))
            {
                GameObject lineGo = new GameObject();
                lineGo.transform.SetParent(transform);
                LineRenderer lineRenderer = lineGo.AddComponent<LineRenderer>();
                //개수
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position + new Vector3(0, 0, -0.55f));
                lineRenderer.SetPosition(1, hTiles.transform.position + new Vector3(0, 0, -0.55f));
                //두께
                lineRenderer.startWidth = 0.5f;
                lineRenderer.endWidth = 0.5f;
                //둥글게
                lineRenderer.numCapVertices = 90;
                
                lineRenderers.Add(hTiles.noiseValue, lineRenderer);
            }
        }
    }

    public void DisConnectTileAll(HTiles hTiles)
    {
        //현재 타일의 연결된 모든 노드 타일을 끊어줌
        foreach (HTiles hTile in hTiles.aroundTiles.Values)
        {
            hTile.DisConncetFromAroundTile(hTiles);
            if (lineRenderers.TryGetValue(hTile.noiseValue, out LineRenderer lineRenderer))
            {
                lineRenderers.Remove(hTile.noiseValue);
                Destroy(lineRenderer.gameObject);
            }
        }
    }
    
    public void DisConncetFromAroundTile(HTiles hTiles)
    {
        if (aroundTiles.ContainsKey(hTiles.noiseValue))
        {
            aroundTiles.Remove(hTiles.noiseValue);
            if (lineRenderers.TryGetValue(hTiles.noiseValue, out LineRenderer lineRenderer))
            {
                lineRenderers.Remove(hTiles.noiseValue);
                Destroy(lineRenderer.gameObject);
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , cellSize* .2f);
    }
}
