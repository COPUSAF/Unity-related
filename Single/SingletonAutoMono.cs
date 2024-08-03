using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 自动挂载式 继承Mono的单例模式基类
/// 懒汉
/// 使用时自动在场景上创建挂载此脚本的空物体 
/// 无需手动挂载 无需动态添加 无需关心切场景带来的问题
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonAutoMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).ToString();
                instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

}
