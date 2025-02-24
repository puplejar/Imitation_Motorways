using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HTileList : MonoBehaviour
{
    public SOTile[] soTiles;
    public int startWayCount = 20;
    public HWayType currentHWayType = HWayType.None;

    private List<HTilePanel> hTilePanels = new List<HTilePanel>();
    
    void Start()
    {
        TilesInit();
    }

    void TilesInit()
    {
        soTiles = new SOTile[Enum.GetValues(typeof(HWayType)).Length];
        string[] names = Enum.GetNames(typeof(HWayType));
        
        GameObject panelOrigin = Util.Load<GameObject>("Prefabs/Panel");
        
        for (int i = 0; i < soTiles.Length - 1; i++)
        {
            soTiles[i] = Util.Load<SOTile>($"SOTiles/{names[i]}");

            GameObject panel = Instantiate(panelOrigin, transform);
            HTilePanel panelComponent = panel.GetComponent<HTilePanel>();
            hTilePanels.Add(panelComponent);
            panelComponent.soTile = soTiles[i];
            panelComponent.hTileList = this;
        }

        hTilePanels[(int)HWayType.Way].tileCount = startWayCount;

    }
}
