using System;
using Regulus.Remote;

namespace Tests
{
    internal class CourtBinder : Regulus.Remote.ISoulBinder 
    {
        public Regulus.Game.Tennis1.Protocol.IPlayground Playground;
        public Regulus.Game.Tennis1.Protocol.IPreparable Preparable;
        public readonly System.Collections.Generic.List<Regulus.Game.Tennis1.Protocol.IPlayer> Players;
        public Regulus.Game.Tennis1.Protocol.IControll Controll;
        public CourtBinder()
        {
            Players = new System.Collections.Generic.List<Regulus.Game.Tennis1.Protocol.IPlayer>();
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
            if (soul as Regulus.Game.Tennis1.Protocol.IPreparable!= null)
                Preparable = soul as Regulus.Game.Tennis1.Protocol.IPreparable;
            if (soul as Regulus.Game.Tennis1.Protocol.IPlayground != null)
                Playground = soul as Regulus.Game.Tennis1.Protocol.IPlayground;            
            if (soul as Regulus.Game.Tennis1.Protocol.IPlayer!=null)
                Players.Add(soul as Regulus.Game.Tennis1.Protocol.IPlayer);
            if (soul as Regulus.Game.Tennis1.Protocol.IControll != null)
                Controll = soul as Regulus.Game.Tennis1.Protocol.IControll;
        }

        void ISoulBinder.Return<TSoul>(TSoul soul)
        {
            
        }

        void ISoulBinder.Unbind<TSoul>(TSoul soul)
        {
            
        }

        
    }
}