using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{

    public static GameObject Load(string path)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        return obj;
    }
}
