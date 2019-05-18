using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// https://github.com/FuzzyHobo/UnityCallbackAndEventTutorial/blob/master/Assets/Scenes/EventCallbackScene/Event.cs

namespace PetProject
{
    // Use this static class to access static listeners for all events 
    // (TODO: Find something better for static event listeners)
    public static partial class GameEventLibrary { }

    // Common types of GameEventArgs
    public struct EmptyEventArgs { };
    public struct GameObjectEventArgs
    {
        public GameObject GameObject;
        public GameObjectEventArgs(GameObject obj) { GameObject = obj; }
    }
    public struct FloatEventArgs
    {
        public float Float;
        public FloatEventArgs(float f) { Float = f; }
    }
    public struct StringEventArgs
    {
        public string String;
        public StringEventArgs(string s) { String = s; }
    }

    public abstract class GameEvent<T> where T : struct
    {
        public delegate void EventListener(T info);

        private event EventListener _listeners;

        public void RegisterListener(EventListener listener)
        {
            _listeners += listener;
        }

        public void UnregisterListener(EventListener listener)
        {
            _listeners -= listener;
        }

        public void FireEvent(T info)
        {
            _listeners?.Invoke(info);
        }
    }
}

