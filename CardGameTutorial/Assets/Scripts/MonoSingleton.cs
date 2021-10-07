using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mono单例模式
/// </summary>
/// <typeparam name="T">泛型</typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError(message: "场景中未找到类的引用对象。类名为：" + typeof(T).Name);
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}
