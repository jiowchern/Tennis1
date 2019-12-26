                    

namespace Tennis1.Common.Adsorption
{
    using System.Linq;
        
    public class PlaygroundAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IPlayground>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Tennis1.Tennis1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Tennis1.Common.IPlayground> {}
        public UnitySupplyEvent SupplyEvent;
        Tennis1.Common.IPlayground _Playground;                        
       
        public PlaygroundAdsorber()
        {
            _Machine = new Regulus.Utility.StageMachine();
        }

        public void Start()
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
            _Agent.Distributor.Attach<IPlayground>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IPlayground>(this);
        }

        private void _ScanLeave()
        {

        }


        private void _ScanEnter()
        {

        }

        public void Update()
        {
            _Machine.Update();
        }

        public void OnDestroy()
        {
            _Machine.Termination();
        }

        public Tennis1.Common.IPlayground GetGPI()
        {
            return _Playground;
        }
        public void Supply(Tennis1.Common.IPlayground gpi)
        {
            if(_Playground != null)
                return;

            _Playground = gpi;
            
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Tennis1.Common.IPlayground gpi)
        {
            if(_Playground != gpi)
                return;

            EnableEvent.Invoke(false);
            
            _Playground = null;
        }
        
        public void Exit()
        {
            if(_Playground != null)
            {
                _Playground.Exit();
            }
        }
        
        
    }
}
                    