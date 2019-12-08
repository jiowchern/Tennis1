using System;
using Regulus.Game.Tennis1.Protocol;

namespace Regulus.Game.Tennis1.Game
{
    internal class Player : Regulus.Game.Tennis1.Protocol.IPlayer, Regulus.Game.Tennis1.Protocol.IControll
    {
        readonly Regulus.Utility.TimeCounter _MoveInterval;
        Regulus.CustomType.Vector2 _Location;
        Regulus.CustomType.Vector2 _Vector;
        readonly float _Speed;
        public Player()
        {
            _Speed = 1f;
            _MoveInterval = new Utility.TimeCounter();
        }

        public event Action<Move> MoveEvent;

        event Action<Move> IPlayer.MoveEvent
        {
            add
            {
                MoveEvent += value;
            }

            remove
            {
                MoveEvent -= value;
            }
        }

        void IControll.Move(Regulus.CustomType.Vector2 vector)
        {
            
            _Location += _Vector * _MoveInterval.Second * _Speed;
            _Vector = vector;
            _MoveInterval.Reset();

            MoveEvent(new Move(_Location , _Vector , _Speed)) ;
        }
    }
}