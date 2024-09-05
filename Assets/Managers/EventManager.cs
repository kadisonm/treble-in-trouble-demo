using UnityEngine;

namespace Events {
    // Music Notes

    public class NotePressed : IEvent 
    {
        public bool Value { get; set; }
    }

    // Player

    public class PlayerHurt : IEvent
    {
        public int Value { get; set; }
    }

    // Enemies

    public class EnemyTriggered : IEvent 
    {
        public Enemy Value { get; set; }
    }

    public class EnemyHit : IEvent 
    {
        public Enemy Value { get; set; }
    }

    public class EnemyDead : IEvent 
    {
        public Enemy Value { get; set; }
    }    
}

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("EventManager is NULL");

            return _instance;
        }
    }

    public EventBus<IEvent> EventBus { get; set; }

    private void Awake() 
    { 
        _instance = this; 
        EventBus = new EventBus<IEvent>();
    }
}
