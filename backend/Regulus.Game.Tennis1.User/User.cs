using Regulus.Remote;

namespace Regulus.Game.Tennis1.User
{
    public class User : Remote.User
    {
        public readonly IAgent Agent;
        

        public User(IAgent agent) : base(agent)
        {
            this.Agent = agent;            
        }
    }
}
