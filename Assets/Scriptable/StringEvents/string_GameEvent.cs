using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class string_GameEvent : ScriptableObject
{
    private List<string_GameEventListener> listeners =
        new List<string_GameEventListener>();

    public void Raise(string obj)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(obj);
    }

    public void RegisterListener(string_GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(string_GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
