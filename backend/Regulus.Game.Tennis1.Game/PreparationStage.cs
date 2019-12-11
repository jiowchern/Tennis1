using Regulus.Game.Tennis1.Protocol;
using Regulus.Utility;
using System.Linq;

namespace Regulus.Game.Tennis1.Game
{

    public class PreparationStage : Regulus.Utility.IStage
    {
        private readonly User[] _Users;
        private readonly Preparation[] _Preparations;
        public event System.Action DoneOnceEvent;
        public PreparationStage(User[] users)
        {
            _Users = users;
            _Preparations = users.Select( user => new Preparation() ).ToArray() ;
        }

        System.Collections.Generic.IEnumerable<System.Tuple<User , Preparation>> _GetPlayers()
        {
            for(int i = 0; i < _Users.Length; ++i)
            {
                yield return new System.Tuple<User, Preparation>(_Users[i] , _Preparations[i]);
            }
        }


        void IStage.Enter()
        {

            foreach(var player in _GetPlayers())
            {
                player.Item1.Binder.Bind<IPreparable>(player.Item2);
            }


        }

        void IStage.Leave()
        {
            foreach (var player in _GetPlayers())
            {
                player.Item1.Binder.Unbind<IPreparable>(player.Item2);
            }
        }

        void IStage.Update()
        {
            if (_Preparations.Count(c => c.Ready) == _Preparations.Length)
            {
                DoneOnceEvent();
                DoneOnceEvent = () => { };
            }
        }
    }
}