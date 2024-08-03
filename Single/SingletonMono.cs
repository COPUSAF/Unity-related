using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承Mono的单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMono<T>: MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        //若已存在单例对象，则销毁当前新创建的对象
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
        //使依附的对象过场景时就不会被移除
        DontDestroyOnLoad(this.gameObject);
    }
}
