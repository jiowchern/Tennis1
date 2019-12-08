using System;
using System.Linq;
using Regulus.Game.Tennis1.Protocol;
using Regulus.Utility;

namespace Regulus.Game.Tennis1.Game
{
    internal class PlayStage : Regulus.Utility.IStage , IPlayground
    {
        readonly Regulus.Utility.StageMachine _Machine;

        private readonly User[] _Users;
        readonly Player[] _Players;

        public event System.Action DoneOnceEvent;

        public PlayStage(User[] users)
        {
            _Machine = new StageMachine();
            _Users = users;
            _Players = users.Select( user => new Player()).ToArray();
        }
        System.Collections.Generic.IEnumerable<System.Tuple<User, Player>> _GetPlayers()
        {
            for (int i = 0; i < _Users.Length; ++i)
            {
                yield return new System.Tuple<User, Player>(_Users[i], _Players[i]);
            }
        }
        void IStage.Enter()
        {
            foreach(var player in _Players)
            {
                foreach (var user in _Users)
                {
                    user.Binder.Bind<Regulus.Game.Tennis1.Protocol.IPlayer>(player);
                    user.Binder.Bind<Regulus.Game.Tennis1.Protocol.IPlayground>(this);
                }
            }

            foreach(var p in _GetPlayers())
            {
                p.Item1.Binder.Bind<Regulus.Game.Tennis1.Protocol.IControll>(p.Item2);
            }
            
        }

        void IPlayground.Exit()
        {
            DoneOnceEvent();
            DoneOnceEvent = () => { };
         }

        void IStage.Leave()
        {
            _Machine.Termination();


            foreach (var player in _Players)
            {
                foreach (var user in _Users)
                {
                    user.Binder.Unbind<Regulus.Game.Tennis1.Protocol.IPlayer>(player);
                    user.Binder.Unbind<Regulus.Game.Tennis1.Protocol.IPlayground>(this);
                }
            }

            foreach (var p in _GetPlayers())
            {
                p.Item1.Binder.Unbind<Regulus.Game.Tennis1.Protocol.IControll>(p.Item2);
            }
        }

        void IStage.Update()
        {
            _Machine.Update();
        }
    }
}