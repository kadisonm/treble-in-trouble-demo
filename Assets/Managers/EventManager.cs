// Music Notes

public class NotePressedEvent : IEvent 
{
    public bool Value { get; set; }
}

// Enemies

public class EnemyTriggered : IEvent 
{
    public Enemy Value { get; set; }
}