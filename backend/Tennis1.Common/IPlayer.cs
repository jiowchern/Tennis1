namespace Tennis1.Common
{
    public interface IPlayer
    {
        event System.Action<Move> MoveEvent;
    }
}
