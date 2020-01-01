using System;
using Tennis1.Common;

namespace Tennis1.Game
{
    internal class Player : Tennis1.Common.IPlayer, Tennis1.Common.IControll
    {
        readonly Regulus.Utility.TimeCounter _MoveInterval;
        Regulus.CustomType.Vector2 _Location;
        Regulus.CustomType.Vector2 _Vector;
        readonly float _Speed;
        public readonly Guid Id;
        Guid IPlayer.Id => Id;

        public Player()
        {
            Id = Guid.NewGuid();
            _Speed = 1f;
            _MoveInterval = new Regulus.Utility.TimeCounter();
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