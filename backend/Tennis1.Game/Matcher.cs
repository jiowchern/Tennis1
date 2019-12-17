using Tennis1.Common;
using System;
using System.Linq;

namespace Tennis1.Game
{
    public class Matcher
    {
        public class Contestant  : Tennis1.Common.IMatchable
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
                _User.Binder.Bind<Tennis1.Common.IMatchable>(this);
            }
            internal void End()
            {
                _User.Binder.Unbind<Tennis1.Common.IMatchable>(this);
            }

            void IMatchable.Cancel()
            {
                CancelOnceEvent();
                CancelOnceEvent = () => { };
            }
        }
        readonly System.Collections.Generic.List<Contestant> _Waiters;
        public Action<System.Guid[]> MatchEvent;
        public Action<System.Guid> CancelEvent;

        public Matcher()
        {
            _Waiters = new System.Collections.Generic.List<Contestant>();

        }
        public void Join(User user)
        {
            var contestant = new Contestant(user);            
            contestant.CancelOnceEvent += () =>
            {
                Left(contestant.Id);
                CancelEvent(contestant.Id);
            };
            contestant.Start();
            _Waiters.Add(contestant);

            var numberGroups = from waiter in _Waiters group waiter by waiter.PlayerNumber;
            foreach(var numberGroup in numberGroups)
            {
                var playerNumber = numberGroup.Key;
                var playerCount = numberGroup.Count();
                var teamCount = playerCount / playerNumber;
                for (var i= 0;i < teamCount; i++)
                {
                    var team = numberGroup.Skip(i * playerNumber).Take(playerNumber);
                    var ids = team.Select(c => c.Id).ToArray();
                    foreach(var id in ids)
                    {
                        Left(id);
                    }
                    MatchEvent(ids);
                }
                
            }
        }

        public int Left(System.Guid id)
        {
            var contestant = _Waiters.Find((w) => w.Id == id);
            if (contestant== null )
            {
                return 0;
            }
            contestant.End();
            return _Waiters.RemoveAll((c) => c.Id == id );
        }
    }
}