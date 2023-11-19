using System;
using System.Collections.Generic;

/// <summary>
/// �C�x���g�������S
/// </summary>
public class EventCenter
{
    private static Dictionary<string, Action> eventNoParamDictionary = new Dictionary<string, Action>();

    private static Dictionary<string, Action<object>> eventWithParamDictionary = new Dictionary<string, Action<object>>();
    /// <summary>
    /// �C�x���g���T�u�X�N���C�u
    /// </summary>
    /// <param name="eventName">�C�x���g�̃L�[</param>
    /// <param name="listener">�C�x���g</param>
    public static void Subscribe(string eventName,Action listener)
    {
        if(eventNoParamDictionary.TryGetValue(eventName,out var thisEvent))
        {
            thisEvent += listener;
            eventNoParamDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventNoParamDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// ���������C�x���g���T�u�X�N���C�u
    /// </summary>
    /// <param name="eventName">�C�x���g�̃L�[</param>
    /// <param name="listener">�C�x���g</param>
    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (eventWithParamDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent += listener;
            eventWithParamDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventWithParamDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// �T�u�X�N���C�u������
    /// </summary>
    /// <param name="eventName">�C�x���g�̃L�[</param>
    /// <param name="listener">�C�x���g</param>
    public static void Unsubscribe(string eventName,Action listener)
    {
        if(eventNoParamDictionary.TryGetValue(eventName,out var thisEvent))
        {
            thisEvent -= listener;
            eventNoParamDictionary[eventName] = thisEvent;
        }
    }

    /// <summary>
    /// ���������T�u�X�N���C�u������
    /// </summary>
    /// <param name="eventName">�C�x���g�̃L�[</param>
    /// <param name="listener">�C�x���g</param>
    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventWithParamDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent -= listener;
            eventWithParamDictionary[eventName] = thisEvent;
        }
    }

    /// <summary>
    /// �C�x���g�����s����
    /// </summary>
    /// <param name="eventName">�C�x���g�̃L�[</param>
    public static void TriggerEvent(string eventName)
    {
        if(eventNoParamDictionary.TryGetValue(eventName,out var thisEvent))
        {
            //���݂���Ȃ���s����
            thisEvent?.Invoke();
        }
    }

    public static void TriggerEvent(string eventName, object eventParam)
    {
        if (eventWithParamDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent?.Invoke(eventParam);
        }
    }
}