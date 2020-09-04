using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour, ISingleton<T> where T : MonoBehaviour
{
    private static T instance;
    public static T Instance { get { return instance; } set { instance = value; } }
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

