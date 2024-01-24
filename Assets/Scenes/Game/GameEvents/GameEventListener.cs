using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

public class GameEventListener : MonoBehaviour
{
    public GameEvent[] gameEvents;
    public CustomGameEvent response;

    private void OnEnable()
    {
        //gameEvent.RegisterListener(this);
        foreach(GameEvent gameEvent in gameEvents)
        {
            gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        //gameEvent.UnregisterListener(this);
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
