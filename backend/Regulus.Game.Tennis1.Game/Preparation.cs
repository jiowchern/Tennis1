using Regulus.Game.Tennis1.Protocol;

namespace Regulus.Game.Tennis1.Game
{
    internal class Preparation : IPreparable
    {
        public bool Ready { get; private set; }
        public Preparation()
        {
            Ready = false;
        }
        void IPreparable.Ready()
        {
            Ready = true;
        }
    }
}