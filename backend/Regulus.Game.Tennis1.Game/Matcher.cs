using Regulus.Game.Tennis1.Protocol;
using System;
using System.Linq;

namespace Regulus.Game.Tennis1.Game
{
    public class Matcher
    {
        public class Contestant  : Regulus.Game.Tennis1.Protocol.IMatchable
        {
            private readonly User _User;
            public readonly System.Guid Id;
            public readonly int PlayerNumber;
            public Contestant(User user)
            {
                this._User = user;
                Id = user.Id;
                PlayerNumber = user.Registration.PlayerNumber;
            }

            public event System.Action CancelOnceEvent;
            internal void Start()
            {
                _User.Binder.Bind<Regulus.Game.Tennis1.Protocol.IMatchable>(this);
            }
            internal void End()
            {
                _User.Binder.Unbind<Regulus.Game.Tennis1.Protocol.IMatchable>(this);
            }

            void IMatchable.Cancel()
            {
                CancelOnceEvent();
                CancelOnceEvent = () => { };
            }
        }
        readonly System.Collections.Generic.List<Contestant> _Waiter;
        public Action<System.Guid[]> MatchEvent;
        public Action<System.Guid> CancelEvent;

        public Matcher()
        {
            _Waiter = new System.Collections.Generic.List<Contestant>();

        }
        public void Join(User user)
        {
            var contestant = new Contestant(user);
            contestant.Start();
            
            if (_Waiter.Count > 0)
            {                
                var opponent = _Waiter[0];
                Left(opponent.Id);                
                
                contestant.End();
                
                MatchEvent.Invoke(new System.Guid[] { opponent.Id, contestant.Id });
            }
            else
            {
                
                contestant.CancelOnceEvent += () =>
                {
                    Left(contestant.Id);
                    CancelEvent(contestant.Id);
                };                
                _Waiter.Add(contestant);
            }
        }

        public int Left(System.Guid id)
        {
            var contestant = _Waiter.Find((w) => w.Id == id);
            if (contestant== null )
            {
                return 0;
            }
            contestant.End();
            return _Waiter.RemoveAll((c) => c.Id == id );
        }
    }
}