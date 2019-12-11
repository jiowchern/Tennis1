using Regulus.Game.Tennis1.Protocol;
using Regulus.Remote;
using System;

namespace Tests
{
    internal class LoungeBinder : ISoulBinder 
    {
        public Regulus.Game.Tennis1.Protocol.IPreparer Lounge;
        public bool IsUnbind { get; private set; }
        event Action ISoulBinder.BreakEvent
        {
            add
            {
            }

            remove
            {

            }
        }
        
        void ISoulBinder.Bind<TSoul>(TSoul soul)
        {
            IsUnbind = false;
            Lounge = soul as Regulus.Game.Tennis1.Protocol.IPreparer;


        }
        
        
        void ISoulBinder.Return<TSoul>(TSoul soul)
        {
            
        }

        

        void ISoulBinder.Unbind<TSoul>(TSoul soul)
        {
            IsUnbind = true;
        }
    }
}