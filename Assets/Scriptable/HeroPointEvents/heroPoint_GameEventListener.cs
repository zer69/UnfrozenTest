using UnityEngine;
using UnityEngine.Events;

public class heroPoint_GameEventListener : MonoBehaviour
{
    public heroPoint_GameEvent Event;
    public UnityEvent<HeroPointPair> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(HeroPointPair obj)
    { Response.Invoke(obj); }
}

