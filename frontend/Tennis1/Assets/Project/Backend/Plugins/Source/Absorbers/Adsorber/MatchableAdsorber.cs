                    

namespace Tennis1.Common.Adsorption
{
    using System.Linq;
        
    public class MatchableAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IMatchable>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Tennis1.Tennis1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Tennis1.Common.IMatchable> {}
        public UnitySupplyEvent SupplyEvent;
        Tennis1.Common.IMatchable _Matchable;                        
       
        public MatchableAdsorber()
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
            _Agent.Distributor.Attach<IMatchable>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IMatchable>(this);
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

        public Tennis1.Common.IMatchable GetGPI()
        {
            return _Matchable;
        }
        public void Supply(Tennis1.Common.IMatchable gpi)
        {
            if (_Matchable != null)
                return;
            _Matchable = gpi;            
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Tennis1.Common.IMatchable gpi)
        {
            if(_Matchable == gpi)
            {
                EnableEvent.Invoke(false);

                _Matchable = null;
            }
            
        }
        
        public void Cancel()
        {
            if(_Matchable != null)
            {
                _Matchable.Cancel();
            }
        }
        
        
    }
}
                    