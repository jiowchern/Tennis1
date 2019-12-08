namespace Regulus.Game.Tennis1.Game
{
    public class Lounge
    {
        public interface IPlayable : Identifiable
        {
            event System.Action<string> SignUpOnceEvent;
        }

        public event System.Action<IPlayable,string> ChallengeEvent;

        readonly System.Collections.Generic.List<IPlayable> _Players;
        public Lounge()
        {
            _Players = new System.Collections.Generic.List<IPlayable>();
        }        

        public int Left(System.Guid id)
        {
            lock(_Players)
            {
                return _Players.RemoveAll((p) => id == p.Id);
            }
            
        }
        public void Join(IPlayable playable)
        {
            playable.SignUpOnceEvent += (name)=>
            {
                ChallengeEvent.Invoke(playable, name);
            };
            lock (_Players)
            {
                _Players.Add(playable);
            }
                
        }
    }
}