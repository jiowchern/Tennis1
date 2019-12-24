                    

namespace Tennis1.Common.Adsorption
{
    using System.Linq;
        
    public class PreparableAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IPreparable>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Tennis1.Tennis1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Tennis1.Common.IPreparable> {}
        public UnitySupplyEvent SupplyEvent;
        Tennis1.Common.IPreparable _Preparable;                        
       
        public PreparableAdsorber()
        {
            _Machine = new Regulus.Utility.StageMachine();
        }

        void Start()
        {
            _Machine.Push(new Regulus.Utility.SimpleStage(_ScanEnter, _ScanLeave, _ScanUpdate));
        }

        private void _ScanUpdate()
        {
            var agents = UnityEngine.GameObject.FindObjectsOfType<global::Tennis1.Tennis1Agent>();
            _Agent = agents.FirstOrDefault(d => string.IsNullOrEmpty(d.Name) == false && d.Name == Agent);
            if(_Agent != null)
            {
                _Machine.Push(new Regulus.Utility.SimpleStage(_DispatchEnter, _DispatchLeave));
            }            
        }

        private void _DispatchEnter()
        {
            _Agent.Distributor.Attach<IPreparable>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IPreparable>(this);
        }

        private void _ScanLeave()
        {

        }


        private void _ScanEnter()
        {

        }

        void Update()
        {
            _Machine.Update();
        }

        void OnDestroy()
        {
            _Machine.Termination();
        }

        public Tennis1.Common.IPreparable GetGPI()
        {
            return _Preparable;
        }
        public void Supply(Tennis1.Common.IPreparable gpi)
        {
            _Preparable = gpi;
            
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Tennis1.Common.IPreparable gpi)
        {
            EnableEvent.Invoke(false);
            
            _Preparable = null;
        }
        
        public void Ready()
        {
            if(_Preparable != null)
            {
                _Preparable.Ready();
            }
        }
        
        
    }
}
                    