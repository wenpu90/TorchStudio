using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoSingleton<T> : MonoBehaviour, ISingleton<T> where T : MonoBehaviour, new() 
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject().AddComponent<T>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    public static bool InstanceNotNull => instance != null;
    public void SetInstance(T t)
    {
        if (instance == null) { instance = t; }
    }

    public void SetInstance(T t, string _path)
    {
        if (instance == null) { GameObject.Find(_path).TryGetComponent(out T instance); }
    }

    public static void Release()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        Release();
    }
}
