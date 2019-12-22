using System;
using Regulus.Remote;

namespace Tests
{
    internal class CourtBinder : Regulus.Remote.IBinder 
    {
        public Tennis1.Common.IPlayground Playground;
        public Tennis1.Common.IPreparable Preparable;
        public readonly System.Collections.Generic.List<Tennis1.Common.IPlayer> Players;
        public Tennis1.Common.IControll Controll;
        public CourtBinder()
        {
            Players = new System.Collections.Generic.List<Tennis1.Common.IPlayer>();
        }

        event Action IBinder.BreakEvent
        {
            add
            {
                
            }

            remove
            {
                
            }
        }

        void IBinder.Bind<TSoul>(TSoul soul)
        {
            if (soul as Tennis1.Common.IPreparable!= null)
                Preparable = soul as Tennis1.Common.IPreparable;
            if (soul as Tennis1.Common.IPlayground != null)
                Playground = soul as Tennis1.Common.IPlayground;            
            if (soul as Tennis1.Common.IPlayer!=null)
                Players.Add(soul as Tennis1.Common.IPlayer);
            if (soul as Tennis1.Common.IControll != null)
                Controll = soul as Tennis1.Common.IControll;
        }

        void IBinder.Return<TSoul>(TSoul soul)
        {
            
        }

        void IBinder.Unbind<TSoul>(TSoul soul)
        {
            
        }

        
    }
}