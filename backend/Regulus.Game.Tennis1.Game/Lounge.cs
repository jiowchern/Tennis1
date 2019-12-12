namespace Regulus.Game.Tennis1.Game
{
    public class Lounge
    {
        

        public event System.Action<System.Guid, Regulus.Game.Tennis1.Protocol.Registration> ChallengeEvent;

        readonly System.Collections.Generic.List<Preparer> _Players;
        public Lounge()
        {
            _Players = new System.Collections.Generic.List<Preparer>();
        }
        
        public int Left(System.Guid id)
        {
            lock (_Players)
            {
                var player = _Players.Find(p => p.Id == id);
                if (player == null )
                {
                    return 0;
                }
                player.End();
                return _Players.RemoveAll((p) => id == p.Id);
            }
            
        }
        public void Join(User user)
        {
            var player = new Preparer(user);
            
            player.SignUpOnceEvent += (registration) =>
            {
                Left(user.Id);
                ChallengeEvent.Invoke(user.Id, registration);
            };
            player.Start();
            lock (_Players)
            {
                _Players.Add(player);
            }
                
        }
    }
}