using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件类型 枚举
/// 每要使用新的事件，就要在这里新建对应的枚举
/// </summary>
public enum E_EventType 
{
    /// <summary>
    /// 玩家获取奖励 —— 参数：int
    /// </summary>
    E_Player_GetReward,
    /// <summary>
    /// 测试用事件 —— 参数：无
    /// </summary>
    E_Test,
    /// <summary>
    /// 对话系统 —— 对话选项被按下
    /// </summary>
    E_DialogueButtonDown,
}
