using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class heroPoint_GameEvent : ScriptableObject
{
    private List<heroPoint_GameEventListener> listeners =
        new List<heroPoint_GameEventListener>();

    public void Raise(HeroPointPair obj)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(obj);
    }

    public void RegisterListener(heroPoint_GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(heroPoint_GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
