using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis1.Common
{
    public struct Registration
    {
        public string Name;
        public int PlayerNumber;
    }
    public interface IPreparer
    {
        void SignUp(Registration registration);
    }
}
