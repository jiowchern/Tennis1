using System;

namespace Regulus.Game.Tennis1.Game
{
    public class Matcher
    {
        public interface IContestant : Identifiable
        {
            event System.Action CancelOnceEvent;
        }
        readonly System.Collections.Generic.List<IContestant> _Waiter;
        public Action<IContestant[]> MatchEvent;
        public Action<IContestant> CancelEvent;

        public Matcher()
        {
            _Waiter = new System.Collections.Generic.List<IContestant>();

        }
        public void Join(IContestant contestant)
        {
            if (_Waiter.Count > 0)
            {                
                var opponent = _Waiter[0];
                _Waiter.RemoveAt(0);
                MatchEvent.Invoke(new IContestant[] { opponent , contestant });
            }
            else
            {
                contestant.CancelOnceEvent += () =>
                {
                    CancelEvent(contestant);
                };
                _Waiter.Add(contestant);
            }
        }

        public int Left(System.Guid id)
        {
            return _Waiter.RemoveAll((c) => c.Id == id );
        }
    }
}