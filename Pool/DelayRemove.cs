using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 挂在到对象池使用的对象上,定时将对象放入对象池
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
