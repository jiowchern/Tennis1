using Regulus.Remote;

namespace Tennis1.User
{
    public class User : Regulus.Remote.User
    {
        public readonly IAgent Agent;
        

        public User(IAgent agent) : base(agent)
        {
            this.Agent = agent;            
        }
    }
}
