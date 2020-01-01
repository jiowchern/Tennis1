namespace Tennis1.Common
{
    public interface IPlayer
    {
        System.Guid Id { get;}
        event System.Action<Move> MoveEvent;
    }
}
