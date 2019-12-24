                    

namespace Tennis1.Common.Adsorption
{
    using System.Linq;
        
    public class PreparerAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IPreparer>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Tennis1.Tennis1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Tennis1.Common.IPreparer> {}
        public UnitySupplyEvent SupplyEvent;
        Tennis1.Common.IPreparer _Preparer;                        
       
        public PreparerAdsorber()
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
            _Agent.Distributor.Attach<IPreparer>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IPreparer>(this);
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

        public Tennis1.Common.IPreparer GetGPI()
        {
            return _Preparer;
        }
        public void Supply(Tennis1.Common.IPreparer gpi)
        {
            _Preparer = gpi;
            
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Tennis1.Common.IPreparer gpi)
        {
            EnableEvent.Invoke(false);
            
            _Preparer = null;
        }
        
        public void SignUp(Tennis1.Common.Registration registration)
        {
            if(_Preparer != null)
            {
                _Preparer.SignUp(registration);
            }
        }
        
        
    }
}
                    