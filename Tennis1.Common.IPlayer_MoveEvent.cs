
    using System;  
    using System.Collections.Generic;
    
    namespace Tennis1.Common.Invoker.IPlayer 
    { 
        public class MoveEvent : Regulus.Remote.IEventProxyCreator
        {

            Type _Type;
            string _Name;
            
            public MoveEvent()
            {
                _Name = "MoveEvent";
                _Type = typeof(Tennis1.Common.IPlayer);                   
            
            }
            Delegate Regulus.Remote.IEventProxyCreator.Create(Guid soul_id,int event_id, Regulus.Remote.InvokeEventCallabck invoke_Event)
            {                
                var closure = new Regulus.Remote.GenericEventClosure<Tennis1.Common.Move>(soul_id , event_id , invoke_Event);                
                return new Action<Tennis1.Common.Move>(closure.Run);
            }
        

            Type Regulus.Remote.IEventProxyCreator.GetType()
            {
                return _Type;
            }            

            string Regulus.Remote.IEventProxyCreator.GetName()
            {
                return _Name;
            }            
        }
    }
                