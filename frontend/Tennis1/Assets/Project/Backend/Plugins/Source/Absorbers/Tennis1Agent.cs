
using System;

using Regulus.Utility;

using UnityEngine;


namespace Tennis1{ 
    public abstract class Tennis1Agent : MonoBehaviour
    {
        private Regulus.Remote.Unity.Distributor _Distributor ;
        public Regulus.Remote.Unity.Distributor Distributor { get{ return _Distributor ; } }
        private readonly Regulus.Utility.Updater _Updater;

        private Regulus.Remote.IAgent _Agent;
        public string Name;
        public Tennis1Agent()
        {            
            _Updater = new Updater();
        }
        public abstract Regulus.Remote.IAgent _GetAgent();
        void Start()   
        {
            _Agent = _GetAgent();
            _Distributor  = new Regulus.Remote.Unity.Distributor(_Agent);
            _Updater.Add(_Agent);
        }
        public void Connect(string ip,int port)
        {            
            _Agent.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port)).OnValue += _ConnectResult;
        }
        
        

        private void _ConnectResult(bool obj)
        {
            ConnectEvent.Invoke(obj);
        }

        void OnDestroy()
        {
            _Updater.Shutdown();
        }

       
        void Update()
        {
            _Updater.Working();
        }
        [Serializable]
        public class UnityAgentConnectEvent : UnityEngine.Events.UnityEvent<bool>{}

        public UnityAgentConnectEvent ConnectEvent;
    }
}
