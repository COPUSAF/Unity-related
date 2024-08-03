using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���ڵ������ʹ�õĶ�����,��ʱ�������������
/// </summary>
public class DelayRemove : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 1f;
    private void OnEnable()
    {
        Invoke("RemoveMe", delayTime);
    }

    private void RemoveMe()
    {
        PoolMgr.Instance.PushObj(this.gameObject);
    }
}
