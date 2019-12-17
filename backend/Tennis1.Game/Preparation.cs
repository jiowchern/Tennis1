using Tennis1.Common;

namespace Tennis1.Game
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