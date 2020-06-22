using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance { get { return _instance; } }

    public static bool IsInitialized
    {
        get { return _instance != null; }
    }

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)this;
            // DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("[Singleton] Attempting to create another instance of a singleton class. Destroying...");
            Destroy(this);
        }


    }

    protected void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
