                    

namespace Tennis1.Common.Adsorption
{
    using System.Linq;
        
    public class PlayerAdsorber : UnityEngine.MonoBehaviour , Regulus.Remote.Unity.Adsorber<IPlayer>
    {
        private readonly Regulus.Utility.StageMachine _Machine;        
        
        public string Agent;

        private global::Tennis1.Tennis1Agent _Agent;

        [System.Serializable]
        public class UnityEnableEvent : UnityEngine.Events.UnityEvent<bool> {}
        public UnityEnableEvent EnableEvent;
        [System.Serializable]
        public class UnitySupplyEvent : UnityEngine.Events.UnityEvent<Tennis1.Common.IPlayer> {}
        public UnitySupplyEvent SupplyEvent;
        Tennis1.Common.IPlayer _Player;                        
       
        public PlayerAdsorber()
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
            _Agent.Distributor.Attach<IPlayer>(this);
        }

        private void _DispatchLeave()
        {
            _Agent.Distributor.Detach<IPlayer>(this);
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

        public Tennis1.Common.IPlayer GetGPI()
        {
            return _Player;
        }
        public void Supply(Tennis1.Common.IPlayer gpi)
        {
            _Player = gpi;
            _Player.MoveEvent += _OnMoveEvent;
            EnableEvent.Invoke(true);
            SupplyEvent.Invoke(gpi);
        }

        public void Unsupply(Tennis1.Common.IPlayer gpi)
        {
            EnableEvent.Invoke(false);
            _Player.MoveEvent -= _OnMoveEvent;
            _Player = null;
        }
        
        
        
        [System.Serializable]
        public class UnityMoveEvent : UnityEngine.Events.UnityEvent<Tennis1.Common.Move> { }
        public UnityMoveEvent MoveEvent;
        
        
        private void _OnMoveEvent(Tennis1.Common.Move arg0)
        {
            MoveEvent.Invoke(arg0);
        }
        
    }
}
                    