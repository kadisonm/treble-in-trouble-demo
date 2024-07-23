using System;
using System.Collections.Generic;

public interface IEvent {}

public class EventBus<TBaseEvent>
{
    private Dictionary<Type, object> listenerClasses = new Dictionary<Type, object>();

    public delegate void EventHandler<T>(T eventData) where T : TBaseEvent;

    public void Subscribe<T>(EventHandler<T> handler) where T : TBaseEvent
    {
        GetListenerClass<T>().AddListener(handler);
    }

    public void Unsubscribe<T>(EventHandler<T> handler) where T : TBaseEvent
    {
        GetListenerClass<T>().RemoveListener(handler);
    }

    public void Publish<T>(T data) where T : TBaseEvent
    {
        List<EventHandler<T>> handlers = new List<EventHandler<T>>();

        foreach (var listener in GetListenerClass<T>().RetrieveListeners())
        {
            handlers.Add(listener.Handler);
        }

        foreach (var handler in handlers)
        {
            handler(data);
        }
    }

    private ListenerClass<T> GetListenerClass<T>() where T : TBaseEvent
    {
        Type type = typeof(T);
        if (!listenerClasses.TryGetValue(type, out object obj))
        {
            ListenerClass<T> listenerClass = new ListenerClass<T>();
            listenerClasses.Add(type, listenerClass);
            return listenerClass;
        }
        return (ListenerClass<T>)obj;
    }
    
    private class ListenerClass <T> where T : TBaseEvent
    {
        public struct Listener 
        {
            public EventHandler<T> Handler;
            
            public Listener(EventHandler<T> handler)
            {
                Handler = handler;
            }
        } 

        private List<Listener> listeners = new List<Listener>();

        public void AddListener(EventHandler<T> handler)
        {
            foreach (var i in listeners)
            {
                if (i.Handler == handler)
                    return;
            }

            Listener listener = new Listener(handler);
            listeners.Add(listener);        
        }

        public void RemoveListener(EventHandler<T> handler)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i].Handler == handler)
                    listeners.Remove(listeners[i]);    
            }
        }

        public List<Listener> RetrieveListeners()
        {
            return listeners;
        }
    }    
}
