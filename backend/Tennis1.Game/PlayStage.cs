using System;
using System.Linq;
using Tennis1.Common;
using Regulus.Utility;

namespace Tennis1.Game
{
    public class PlayStage : Regulus.Utility.IStage , IPlayground
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
                    user.Binder.Bind<Tennis1.Common.IPlayer>(player);
                }
            }

            foreach(var p in _GetPlayers())
            {
                p.Item1.Binder.Bind<Tennis1.Common.IControll>(p.Item2);
                p.Item1.Binder.Bind<Tennis1.Common.IPlayground>(this);

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
                    user.Binder.Unbind<Tennis1.Common.IPlayer>(player);
                }
            }

            foreach (var p in _GetPlayers())
            {
                p.Item1.Binder.Unbind<Tennis1.Common.IControll>(p.Item2);
                p.Item1.Binder.Unbind<Tennis1.Common.IPlayground>(this);

            }
        }

        void IStage.Update()
        {
            _Machine.Update();
        }
    }
}