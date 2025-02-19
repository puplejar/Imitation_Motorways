using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static GameObject Load(string path)
    {
        return Load<GameObject>(path);
    }

    public static T Load<T>(string path) where T : Object
    {
        T load = Resources.Load<T>(path);
        return load as T;
    }
}
