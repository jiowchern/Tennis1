using System;

namespace Regulus.Game.Tennis1.Game
{
    internal class Court
    {
        readonly System.Collections.Generic.List<Round> _Rounds;

        public System.Action<Round> DoneEvent;

        public Court()
        {
            _Rounds = new System.Collections.Generic.List<Round>();
        }
        internal void Join(Round round)
        {
            round.DoneOnceEvent += () =>
            {
                round.Stop();
                DoneEvent(round);
            };
            round.Start();
        }

        internal void Left(Guid id)
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
