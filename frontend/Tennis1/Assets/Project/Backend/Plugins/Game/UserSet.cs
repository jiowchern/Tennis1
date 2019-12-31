namespace Tennis1.Game
{
    internal class UserSet
    {
        readonly System.Collections.Generic.Dictionary<System.Guid, User> _Players;
        public UserSet()
        {
            _Players = new System.Collections.Generic.Dictionary<System.Guid, User>();
        }

        public void Add(User player)
        {
            _Players.Add(player.Id , player);
        }

        public void Remove(System.Guid id)
        {
            _Players.Remove(id);
        }

        public User Find(System.Guid id)
        {
            return _Players[id];
        }

    }
}