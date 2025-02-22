using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEventHandler : MonoBehaviour , IDragHandler , IPointerClickHandler , IPointerUpHandler
{
    
    public HMapGeneratorTool hMapGeneratorTool;
    public HTileList tileList;

    public float rangeCheck = 0.15f;
    
    //드래그 중인 타일리스트 변수
    private List<HTiles> tiles = new List<HTiles>();
    HTiles currentTile;
    
    //타일 안의 범위 체크용 함수 OnMouseEnter와 같은역할
    void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentTile = hit.transform.GetComponent<HTiles>();
            if (currentTile == null) return;
            
            Debug.Log(currentTile.gameObject.transform.position);
            
            //hit된 지형의 타입이 UI의 선택된 타일의 설치가능한 타입인지 체크
            if(currentTile.hTerrainType != tileList.soTiles[(int)tileList.currentHWayType].hTerrainType) return;
            
            if (RangeCheck(hit.point,   hit.transform.position))
            {
                Debug.Log(transform.position);
                currentTile.inSetRange = true;
            }
            else
            {
                currentTile.inSetRange = false;
            }
            
            Debug.Log(currentTile.tile.name + " : " +  currentTile.gameObject.transform.position);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        if (currentTile.inSetRange)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    currentTile.SetTile(tileList.soTiles[(int)tileList.currentHWayType]);
                    break;
                case PointerEventData.InputButton.Right:
                    currentTile.DestroyTile(currentTile);
                    break;
            }
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        if (currentTile.inSetRange || !tiles.Contains(currentTile))
        {
            tiles.Add(currentTile);
            if (tiles.Count <= 1) return;
            
            tiles[tiles.Count - 1].ConnectTIle(tiles[tiles.Count - 2]);
            tiles[tiles.Count - 2].ConnectTIle(tiles[tiles.Count - 1]);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        tiles.Clear();
    }
    
    bool RangeCheck(Vector2 point, Vector2 center)
    {
        return (point.x - center.x) * (point.x - center.x) + 
            (point.y - center.y) * (point.y - center.y) <= hMapGeneratorTool.cellSize * rangeCheck;
    }
}
