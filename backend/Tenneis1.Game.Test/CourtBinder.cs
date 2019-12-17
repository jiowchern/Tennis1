using System;
using Regulus.Remote;

namespace Tests
{
    internal class CourtBinder : Regulus.Remote.ISoulBinder 
    {
        public Tennis1.Common.IPlayground Playground;
        public Tennis1.Common.IPreparable Preparable;
        public readonly System.Collections.Generic.List<Tennis1.Common.IPlayer> Players;
        public Tennis1.Common.IControll Controll;
        public CourtBinder()
        {
            Players = new System.Collections.Generic.List<Tennis1.Common.IPlayer>();
        }

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
            if (soul as Tennis1.Common.IPreparable!= null)
                Preparable = soul as Tennis1.Common.IPreparable;
            if (soul as Tennis1.Common.IPlayground != null)
                Playground = soul as Tennis1.Common.IPlayground;            
            if (soul as Tennis1.Common.IPlayer!=null)
                Players.Add(soul as Tennis1.Common.IPlayer);
            if (soul as Tennis1.Common.IControll != null)
                Controll = soul as Tennis1.Common.IControll;
        }

        void ISoulBinder.Return<TSoul>(TSoul soul)
        {
            
        }

        void ISoulBinder.Unbind<TSoul>(TSoul soul)
        {
            
        }

        
    }
}