using UnityEngine;
using UnityEngine.Events;

public class string_GameEventListener : MonoBehaviour
{
    public string_GameEvent Event;
    public UnityEvent<string> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(string obj)
    { Response.Invoke(obj); }
}

