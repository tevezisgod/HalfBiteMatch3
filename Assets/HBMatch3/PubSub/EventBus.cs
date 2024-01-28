using System;
using System.Collections.Generic;

namespace HBMatch3.PubSub
{
    public class EventBus
    {
        private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

        public void Subscribe(string eventName, Action listener)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = listener;
            }
            else
            {
                eventDictionary[eventName] += listener;
            }
        }

        public void Unsubscribe(string eventName, Action listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
        }

        public void Publish(string eventName)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName].Invoke();
            }
        }
    }
}