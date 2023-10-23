using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class int_GameEvent : ScriptableObject
{
    private List<int_GameEventListener> listeners =
        new List<int_GameEventListener>();

    public void Raise(int obj)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(obj);
    }

    public void RegisterListener(int_GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(int_GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
