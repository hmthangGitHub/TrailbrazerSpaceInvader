using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher
{
    private static EventDispatcher instance;
    private static EventDispatcher Instance => instance ??= new EventDispatcher();
    private readonly Dictionary<Type, List<Delegate>> eventHandlers = new();

    public static void Subscribe<T>(Action<T> handler)
    {
        if (!Instance.eventHandlers.TryGetValue(typeof(T), out var eventHandlerList))
        {
            eventHandlerList = new List<Delegate>();
            Instance.eventHandlers.Add(typeof(T), eventHandlerList);
        }
        eventHandlerList.Add(handler);
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        if (Instance.eventHandlers.TryGetValue(typeof(T), out var eventHandlerList))
        {
            eventHandlerList.Remove(handler);
        }
    }

    public static void Publish<T>(T parameter)
    {
        var delegates = Instance.eventHandlers[typeof(T)].ToArray();
        foreach (var eventHandler in delegates)
        {
            eventHandler.DynamicInvoke(parameter);
        }
    }
}
