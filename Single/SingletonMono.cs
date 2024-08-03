using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̳�Mono�ĵ���ģʽ����
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
        //���Ѵ��ڵ������������ٵ�ǰ�´����Ķ���
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
        //ʹ�����Ķ��������ʱ�Ͳ��ᱻ�Ƴ�
        DontDestroyOnLoad(this.gameObject);
    }
}
