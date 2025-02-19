using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class HTiles : MonoBehaviour , IDragHandler , IPointerClickHandler
{
    private TerrainType _terrainType;

    public TerrainType terrainType
    {
        get { return _terrainType; }
        set
        {
            _terrainType = value;
            Renderer renderer = gameObject.GetComponent<Renderer>();
            
            switch (_terrainType)
            {
                case TerrainType.Ground:
                    //Test Renderer
                    renderer.material.color = Color.white;
                    break;
                case TerrainType.Water:
                    renderer.material.color = Color.blue;
                    break;
                case TerrainType.Bridge:
                    //renderer.material.color = Color.white;
                    break;
            }
        }
    }
    
    public SOTile tile;
    public HTileList tileList; //프라이벳?

    [HideInInspector] public float noiseValue;
    [HideInInspector] public float cellSize = 1;
    private IPointerEnterHandler pointerEnterHandlerImplementation;

    private bool canSet = false;

    private void Start()
    {
        //게임매니저안의 UI매니저 안의 타일리스트가 있을것임
        //타일리스트와 연결해줌
    }

    void OnMouseOver()
    {

        if (RangeCheck(MousePosition(), transform.position))
        {
            Debug.Log(transform.position);
            canSet = true;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canSet)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    SetTile();
                    break;
                case PointerEventData.InputButton.Right:
                    Debug.Log("🖱️ 오른쪽 버튼 클릭!");
                    break;
            }
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (canSet)
        {
            
        }
    }
    public void SetTile()
    {
        if(tileList.currentHWayType == HWayType.None) return;
        
        this.tile = tileList.soTiles[(int)tileList.currentHWayType];
        //함수 안에 만들건데
        //새로 오브젝트를 생성하는것이 아니고
        //기존 타일에서 덧씌울것임
    }

    public void DestroyTile()
    {
        //this.tile = 
    }
    
    Vector3 MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    bool RangeCheck(Vector2 point, Vector2 center)
    {
        return (point.x - center.x) * (point.x - center.x) + 
            (point.y - center.y) * (point.y - center.y) <= cellSize * .2f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , cellSize* .2f);
    }

}
