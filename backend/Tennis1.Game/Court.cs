using System;

namespace Tennis1.Game
{
    public class Court
    {
        readonly System.Collections.Generic.List<Round> _Rounds;

        public System.Action<System.Collections.Generic.IEnumerable<Guid>> DoneEvent;

        public Court()
        {
            _Rounds = new System.Collections.Generic.List<Round>();
        }
        public void Join(System.Collections.Generic.IEnumerable<User> users)
        {
            var round = new Round(users);
            _Rounds.Add(round);
            round.DoneOnceEvent += () =>
            {
                _Left(round);
                DoneEvent(round.ContestantIds);
            };
            round.Start();
        }

        public void Left(Guid id)
        {
            foreach(var round in _Rounds)
            {
                if (round.Has(id))
                {
                    _Left(round);
                    return;
                }
            }
        }

        private void _Left(Round round)
        {
            round.Stop();
            _Rounds.RemoveAll((r) => r.Id == round.Id);
        }
    }
}
