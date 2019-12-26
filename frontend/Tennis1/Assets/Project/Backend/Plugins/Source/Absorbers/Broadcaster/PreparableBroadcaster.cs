
using System;

using System.Linq;

using Regulus.Utility;

using UnityEngine;
using UnityEngine.Events;

namespace Tennis1{ 
    public class PreparableBroadcaster : UnityEngine.MonoBehaviour 
    {
        public string Agent;        
        Regulus.Remote.INotifier<Tennis1.Common.IPreparable> _Notifier;

        private readonly Regulus.Utility.StageMachine _Machine;

        public PreparableBroadcaster()
        {
            _Machine = new StageMachine();
        } 

        public void Start()
        {
            _ToScan();
        }

        private void _ToScan()
        {
            var stage = new Regulus.Utility.SimpleStage(_ScanEnter , _ScaneLeave , _ScaneUpdate);

            _Machine.Push(stage);
        }


        private void _ScaneUpdate()
        {
            var agents = GameObject.FindObjectsOfType<Tennis1.Tennis1Agent>();
            var agent = agents.FirstOrDefault(d => d.Name == Agent);
            if (agent != null)
            {
                _Notifier = agent.Distributor.QueryNotifier<Tennis1.Common.IPreparable>();

                _ToInitial();                                
            }
        }

        private void _ToInitial()
        {
            var stage = new Regulus.Utility.SimpleStage(_Initial);
            _Machine.Push(stage);
        }

        private void _Initial()
        {
            _Notifier.Supply += _Supply;
            _Notifier.Unsupply += _Unsupply;
        }

        private void _ScaneLeave()
        {
            
        }

        private void _ScanEnter()
        {
            
        }

        // Update is called once per frame
        public void Update()
        {
            _Machine.Update();
        }

        public void OnDestroy()
        {
            if (_Notifier != null)
            {
                _Notifier.Supply -= _Supply;
                _Notifier.Unsupply -= _Unsupply;
            }
                
            _Machine.Termination();
        }

        private void _Unsupply(Tennis1.Common.IPreparable obj)
        {
            UnsupplyEvent.Invoke(obj);
        }

        private void _Supply(Tennis1.Common.IPreparable obj)
        {
            SupplyEvent.Invoke(obj);
        }

        [Serializable]
        public class UnityBroadcastEvent : UnityEvent<Tennis1.Common.IPreparable>{}

        public UnityBroadcastEvent SupplyEvent;
        public UnityBroadcastEvent UnsupplyEvent;
    }
}
