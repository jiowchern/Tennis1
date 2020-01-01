   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Tennis1.Common.Ghost 
    { 
        public class CIPlayer : Tennis1.Common.IPlayer , Regulus.Remote.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly Guid _GhostIdName;
            
            
            
            public CIPlayer(Guid id, bool have_return )
            {
                _HaveReturn = have_return ;
                _GhostIdName = id;            
            }
            

            Guid Regulus.Remote.IGhost.GetID()
            {
                return _GhostIdName;
            }

            bool Regulus.Remote.IGhost.IsReturnType()
            {
                return _HaveReturn;
            }
            object Regulus.Remote.IGhost.GetInstance()
            {
                return this;
            }

            private event Regulus.Remote.CallMethodCallback _CallMethodEvent;

            event Regulus.Remote.CallMethodCallback Regulus.Remote.IGhost.CallMethodEvent
            {
                add { this._CallMethodEvent += value; }
                remove { this._CallMethodEvent -= value; }
            }
            
            

                public System.Guid _Id;
                System.Guid Tennis1.Common.IPlayer.Id { get{ return _Id;} }

                public System.Action<Tennis1.Common.Move> _MoveEvent;
                event System.Action<Tennis1.Common.Move> Tennis1.Common.IPlayer.MoveEvent
                {
                    add { _MoveEvent += value;}
                    remove { _MoveEvent -= value;}
                }
                
            
        }
    }
