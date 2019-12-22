using Regulus.Framework;
using Regulus.Remote;
using Regulus.Remote.Standalone;
using Regulus.Utility;

namespace Tennis1.User
{
    public class StandaloneUserFactory : IUserFactoty<User>
    {
        private readonly IEntry _Standalone;


        private readonly System.Reflection.Assembly _ProtocolAssembly;

        public StandaloneUserFactory(IEntry entry, System.Reflection.Assembly assembly)

        {
            _Standalone = entry;
            this._ProtocolAssembly = assembly;
        }


        User IUserFactoty<User>.SpawnUser()
        {

            var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(_ProtocolAssembly);
            var agent = new Agent(protocol);
            agent.ConnectedEvent += () => { this._Standalone.AssignBinder(agent); };

            return new User(agent);

        }

        ICommandParsable<User> IUserFactoty<User>.SpawnParser(Command command, Console.IViewer view, User user)
        {
            return new CommandParser(command, view, user);
        }
    }
}
