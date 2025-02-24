using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HTilePanel : MonoBehaviour,IPointerClickHandler
{
    [HideInInspector]public HTileList hTileList;
    public SOTile soTile;
    
    public Image image;
    public TextMeshProUGUI text;
    
    int _tileCount;
    public int tileCount
    {
        get
        {
            if (_tileCount == 0)
            {
                hTileList.currentHWayType = HWayType.None;
            }
            text.text = _tileCount.ToString();
            return _tileCount;
        }
        set
        {
            _tileCount = value;
            text.text = _tileCount.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + ".OnPointerClick");
        hTileList.currentHWayType = soTile.hWayType;
    }
}
