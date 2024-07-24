//Player Input

public class MoveInputEvent : IEvent 
{
    public float Value { get; set; }
}

public class JumpInputEvent : IEvent 
{
    public bool Value { get; set; }
}

public class ClickInputEvent : IEvent {}

//Player Script Events

public class OnSpriteFlipEvent : IEvent 
{
    public float Value { get; set; }
}

// Player Velocity

public class MoveVelocityEvent : IEvent 
{
    public float Value { get; set; }
}

public class JumpVelocityEvent : IEvent 
{
    public float Value { get; set; }
}

public class DashVelocityEvent : IEvent 
{
    public float Value { get; set; }
}