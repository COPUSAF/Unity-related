using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �Զ�����ʽ �̳�Mono�ĵ���ģʽ����
/// ����
/// ʹ��ʱ�Զ��ڳ����ϴ������ش˽ű��Ŀ����� 
/// �����ֶ����� ���趯̬��� ��������г�������������
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
