using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEventHandler : MonoBehaviour
{
    
    public HMapGeneratorTool hMapGeneratorTool;
    public HTileList tileList;

    public float rangeCheck = 0.15f;
    
    //드래그 중인 타일리스트 변수
    private List<HTiles> tiles = new List<HTiles>();
    HTiles currentTile;

    void Update()
    {
        RangeCheck();
        if (Input.GetMouseButton(0))
        {
            SetTiles();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            tiles.Clear();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DestroyTileByMouse();
        }
    }
    
    //타일 안의 범위 체크용 함수 OnMouseEnter와 같은역할
    void RangeCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentTile = hit.transform.GetComponent<HTiles>();
            if (currentTile == null) return;
            
            //hit된 지형의 타입이 UI의 선택된 타일의 설치가능한 타입인지 체크
            if (currentTile.hTerrainType != tileList.soTiles[(int)tileList.currentHWayType]?.hTerrainType)
            {
                //필요하다면 경고표시 이펙트
                //Bridge가 있다면 해당 if문은 무시하고 타입변환후 셋 타일
                tiles.Clear();
                return;
            }
            
            if (RangeCheck(hit.point,   hit.transform.position))
            {
                currentTile.inSetRange = true;
            }
            else
            {
                currentTile.inSetRange = false;
            }
        }
    }
    public void SetTileByMouse()
    {
        if (currentTile?.inSetRange == true)
        {
            currentTile.SetTile(tileList.soTiles[(int)tileList.currentHWayType]);
        }
    }

    public void DestroyTileByMouse()
    {
        if (currentTile?.inSetRange == true)
        {
            currentTile.DestroyTile(currentTile);
        }
    }

    public void SetTiles()
    {
        if (currentTile?.inSetRange == true && !tiles.Contains(currentTile))
        {
            tiles.Add(currentTile);
            SetTileByMouse();
            if (tiles.Count <= 1) return;
            
            tiles[tiles.Count - 1].ConnectTIle(tiles[tiles.Count - 2]);
            tiles[tiles.Count - 2].ConnectTIle(tiles[tiles.Count - 1]);
        }
    }
    
    bool RangeCheck(Vector2 point, Vector2 center)
    {
        return (point.x - center.x) * (point.x - center.x) + 
            (point.y - center.y) * (point.y - center.y) <= hMapGeneratorTool.cellSize * rangeCheck;
    }
}
