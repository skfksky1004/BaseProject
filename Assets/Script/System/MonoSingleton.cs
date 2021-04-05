using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T I => Instance;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return instance;
        }
    }

    public void SetParent(Transform tf)
    {
        if(tf == null)
            return;
        
        this.transform.SetParent(tf);
    }

    public abstract bool Initialize();
    protected abstract void Destroy();

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        Destroy();
    }
}