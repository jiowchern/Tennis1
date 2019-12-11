using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regulus.Framework;
using Regulus.Remote;

namespace Regulus.Game.Tennis1.Game
{
    public class Entry : Regulus.Remote.IEntry
    {
        readonly UserSet _Users;
        readonly Matcher _Matcher;
        readonly Lounge _Lounge;
        readonly Court _Court;
        public Entry()
        {
            _Users = new UserSet();
            _Lounge = new Lounge();
            _Matcher = new Matcher();
            _Court = new Court();
        }
        void IBinderProvider.AssignBinder(ISoulBinder binder)
        {
            
            var user = new User(binder);
            _Users.Add(user);
            binder.BreakEvent += () =>
            {                
                _Lounge.Left(user.Id);
                _Matcher.Left(user.Id);
                _Court.Left(user.Id);
                _Users.Remove(user.Id);
            };

            _Lounge.Join(user);
        }
        void IBootable.Launch()
        {
            Launch();
        }
        public void Launch()
        {
            _Lounge.ChallengeEvent += (id , name)=> {
                _ToMatch(id, name);
            };

            _Matcher.MatchEvent += (contestants) =>
            {
                _ToCourt(contestants);                
            };

            _Matcher.CancelEvent += (contestant) =>
            {
                _ToLougne(contestant);
            };

            _Court.DoneEvent += (round) => {
                foreach( var contestant in round.ContestantIds)
                {
                    _ToLougne(contestant);
                }
            };
        }

        private void _ToCourt(IEnumerable<System.Guid> ids)
        {
            
            _Court.Join(new Round(ids.Select(id => _Users.Query(id))));
        }

        private void _ToLougne(System.Guid id)
        {
            var user = _Users.Query(id);
            _Lounge.Join(user);
        }

        private void  _ToMatch(System.Guid id, string name) 
        {
            var user = _Users.Query(id);
            user.Name = name;
            _Matcher.Join(user);
        }
        public void Shutdown()
        {

        }
        void IBootable.Shutdown()
        {
            Shutdown();
        }
    }
}
