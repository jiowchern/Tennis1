using Regulus.CustomType;

namespace Tennis1.Common
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
