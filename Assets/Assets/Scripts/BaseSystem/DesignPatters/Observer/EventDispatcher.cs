using UnityEngine;
using System.Collections.Generic;
using System;

namespace BaseSystems.DesignPatterns.Observer
{
    public class EventDispatcher : MonoBehaviour
    {
        #region Init, Singleton, Awake
        /// <summary>
        /// Store all listeners.
        /// </summary>
        Dictionary<ObserverEventID, Action<object>> listenersDict = new Dictionary<ObserverEventID, Action<object>>();

        public static EventDispatcher Instance
        {
            get
            {
                // instance not exist, then create new one
                if (instance == null)
                {
                    // create new Gameobject, and add EventDispatcher component
                    GameObject singletonObject = new GameObject("Observer");
                    instance = singletonObject.AddComponent<EventDispatcher>();
                    Debug.Log(string.Format("Created singleton : {0}", singletonObject.name));
                }
                return instance;
            }
            private set { }
        }
        private static EventDispatcher instance = null;

        void Awake ()
        {
            // check if there's another instance already exist in scene
            if (instance != null && instance.GetInstanceID() != this.GetInstanceID())
            {
                // Destroy this instances because already exist the singleton of Observer
                Debug.Log(string.Format("An instance of Observer already exist : {0}, destroy this instance : {1}!!", instance.name, name));
                DestroyObject(this);
            }
            else
            {
                // set instance
                instance = this as EventDispatcher;
                DontDestroyOnLoad(this);
            }
        }
        #endregion

        #region Register, Post, Remove events
        /// <summary>
        /// Register to listen for eventID.
        /// </summary>
        /// <param name="eventID">EventID that object want to listen</param>
        /// <param name="callback">Callback will be invoked when this eventID be raised</param>
        public void RegisterListener(ObserverEventID eventID, Action<object> callback)
        {
            // checking params
            Debug.Assert(callback != null, string.Format("AddListener, event {0}, callback is null !!", eventID.ToString()));
            Debug.Assert(eventID != ObserverEventID.None, "RegisterListener, event = None !!");

            // check if listener exist in distionary
            if (listenersDict.ContainsKey(eventID))
            {
                // add callback to our collection
                listenersDict[eventID] += callback;
            }
            else
            {
                // add new key-value pair
                listenersDict.Add(eventID, null);
                listenersDict[eventID] += callback;
            }
        }


        /// <summary>
        /// Posts the event. This will notify all listener that has been registered for this event.
        /// </summary>
        /// <param name="eventID">Event that object want to listen.</param>
        /// <param name="param">Parameter. Listener can make a cast to get the data.</param>
		public void PostEvent(ObserverEventID eventID, object param = null)
        {
            if (!listenersDict.ContainsKey(eventID))
            {
                Debug.Log("No listener for this event : " + eventID);
                return;
            }

            Action<object> callbacks = listenersDict[eventID];
            if (callbacks != null)
            {
                callbacks(param);
            }
            else
            {
                Debug.Log("PostEvent " + eventID + " but there is no remaining listener. Remove this key");
                listenersDict.Remove(eventID);
            }
        }

        /// <summary>
        /// Removes the listener. Use to unregister listener.
        /// </summary>
        /// <param name="eventID">Event that object want to listen.</param>
        /// <param name="callback">Callback action.</param>
		public void RemoveListener(ObserverEventID eventID, Action<object> callback)
        {
            // checking params
            Debug.Assert(callback != null, string.Format("RemoveListener, event {0}, callback is null.", eventID.ToString()));
            Debug.Assert(eventID != ObserverEventID.None, "AddListener, event = None.");

            if (listenersDict.ContainsKey(eventID))
            {
                listenersDict[eventID] -= callback;
            }
            else
            {
                Debug.LogWarning("RemoveListener, tried to remove nonexistent key : " + eventID);
            }
        }

        /// <summary>
        /// Clears all the listeners.
        /// </summary>
        public void ClearAllListener()
        {
            listenersDict.Clear();
        }
        #endregion
    }

    #region Extension class
    public static class EventDispatcherExtension
    {
        /// <summary>
        /// Register to listen for eventID.
        /// </summary>
        /// <param name="eventID">EventID that object want to listen</param>
        /// <param name="callback">Callback will be invoked when this eventID be raised</param>
        public static void RegisterListener(this object sender, ObserverEventID eventID, Action<object> callback)
        {
            EventDispatcher.Instance.RegisterListener(eventID, callback);
        }


        /// <summary>
        /// Posts the event. This will notify all listener that register for this event
        /// </summary>
        /// <param name="eventID">Event that object want to listen</param>
        /// <param name="param">Parameter. Listener can make a cast to get the data</param>
        public static void PostEvent(this object sender, ObserverEventID eventID, object param)
        {
            EventDispatcher.Instance.PostEvent(eventID, param);
        }

        /// <summary>
        /// Posts the event. This will notify all listener that register for this event
        /// </summary>
        /// <param name="eventID">Event that object want to listen</param>
        public static void PostEvent(this object sender, ObserverEventID eventID)
        {
            EventDispatcher.Instance.PostEvent(eventID, null);
        }

        /// <summary>
        /// Removes the listener. Use to Unregister listener
        /// </summary>
        /// <param name="eventID">Event that object want to listen.</param>
        /// <param name="callback">Callback action.</param>
        public static void RemoveListener(this object sender, ObserverEventID eventID, Action<object> callback)
        {
            EventDispatcher.Instance.RemoveListener(eventID, callback);
        }
    }
    #endregion
}