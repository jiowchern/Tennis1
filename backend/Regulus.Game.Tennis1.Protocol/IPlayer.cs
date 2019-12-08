namespace Regulus.Game.Tennis1.Protocol
{
    public interface IPlayer
    {
        event System.Action<Move> MoveEvent;
    }
}
