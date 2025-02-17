using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTiles :MonoBehaviour
{
    public bool activeTile = false;
    public SOTile tile;

    public float noiseValue;
    
    //처음 생성

    public void SetTile(SOTile tile, bool active)
    {
        this.tile = tile;
        activeTile = active;
    }
}
