using Regulus.Framework;
using Regulus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis1.User
{
    public partial class RemoteUserFactory : IUserFactoty<User>
    {
        private readonly System.Reflection.Assembly _ProtocolAssembly;

        public RemoteUserFactory(System.Reflection.Assembly assembly)
        {
            this._ProtocolAssembly = assembly;
        }

        User IUserFactoty<User>.SpawnUser()
        {
            return new User(Regulus.Remote.Client.AgentProivder.CreateTcp(_ProtocolAssembly));
        }

        ICommandParsable<User> IUserFactoty<User>.SpawnParser(Command command, Regulus.Utility.Console.IViewer view, User user)
        {
            return new CommandParser(command, view, user);
        }
    }
}
