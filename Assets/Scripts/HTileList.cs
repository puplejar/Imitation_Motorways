using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTileList : MonoBehaviour
{
    public SOTile[] soTiles;
    //타일마다 수량을 가지고 있음
    
    public HWayType currentHWayType = HWayType.None;
    
    void Start()
    {
        soTiles = new SOTile[Enum.GetValues(typeof(HWayType)).Length];
        string[] names = Enum.GetNames(typeof(HWayType));
        for (int i = 0; i < soTiles.Length - 1; i++)
        {
            soTiles[i] = Util.Load<SOTile>($"SOTiles/{names[i]}");
        }

        currentHWayType = HWayType.Way;
    }
}
