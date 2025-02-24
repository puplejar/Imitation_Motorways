using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObject/Tile Data")]

public class SOTile : ScriptableObject
{
    //타일즈의 정보들엔
    //길 , 집, 공장
    //액티브가능한(집을 지을 수 잇는지, 도로를 건설가능한지) 불값
    
    public GameObject hTileObject;
    //유아이-타일리스트에서 쓰임
    public HWayType hWayType;
    //셋타일 전, 지형과 맞는지 확인할 때 쓰임
    public HTerrainType hTerrainType;
    public Sprite sprite;
    public Vector3 Size;
}
