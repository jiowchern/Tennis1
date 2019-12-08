using Regulus.CustomType;

namespace Regulus.Game.Tennis1.Protocol
{
    public class Move
    {
        public readonly Regulus.CustomType.Vector2 Start;
        public readonly Regulus.CustomType.Vector2 Vector;
        public readonly float Speed;

        public Move(Vector2 location, Vector2 vector, float speed)
        {
            Start = location;
            Vector = vector;
            Speed = speed;
        }
    }
}
