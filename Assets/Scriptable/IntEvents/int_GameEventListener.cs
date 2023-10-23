using UnityEngine;
using UnityEngine.Events;

public class int_GameEventListener : MonoBehaviour
{
    public int_GameEvent Event;
    public UnityEvent<int> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(int obj)
    { Response.Invoke(obj); }
}

