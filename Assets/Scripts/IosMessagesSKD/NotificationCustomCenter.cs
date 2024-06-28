using System;
using System.Collections.Generic;
using UnityEngine;

namespace CubeDash.Assets.Scripts.IosMessagesSKD
{
    public class NotificationCustomCenter : MonoBehaviour
    {
        private static NotificationCustomCenter instance;
        private Dictionary<string, Action> eventTable = new Dictionary<string, Action>();

        public static NotificationCustomCenter Instance => instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void AddObserver(string eventName, Action listener)
        {
            if (!eventTable.ContainsKey(eventName))
            {
                eventTable[eventName] = listener;
            }
            else
            {
                eventTable[eventName] += listener;
            }
        }

        public void RemoveObserver(string eventName, Action listener)
        {
            if (eventTable.ContainsKey(eventName))
            {
                eventTable[eventName] -= listener;

                if (eventTable[eventName] == null)
                {
                    eventTable.Remove(eventName);
                }
            }
        }

        public void PostNotification(string eventName)
        {
            if (eventTable.ContainsKey(eventName))
            {
                eventTable[eventName]?.Invoke();
            }
        }
    }
}
