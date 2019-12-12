using Regulus.Framework;
using Regulus.Game.Tennis1.Protocol;
using Regulus.Remote;
using Regulus.Utility;

namespace Regulus.Game.Tennis1.User
{
    internal class CommandParser : ICommandParsable<User>
    {
        private Command _Command;
        private Regulus.Utility.Console.IViewer _Viewer;
        private User _User;

        public CommandParser(Command command, Regulus.Utility.Console.IViewer view, User user)
        {
            this._Command = command;
            this._Viewer = view;
            this._User = user;
        }

        void ICommandParsable<User>.Clear()
        {
            
        }

        void ICommandParsable<User>.Setup(IGPIBinderFactory factory)
        {
            this._CreateConnect(factory);
            this._CreateLounge(factory);
            this._CreateMatch(factory);
            this._CreatePlayer(factory);
            _CreatePlayground(factory);
            _CreateContoll(factory);
        }

        private void _CreateContoll(IGPIBinderFactory factory)
        {
            var gpi = factory.Create(this._User.Agent.QueryNotifier<Regulus.Game.Tennis1.Protocol.IControll>());
            gpi.Bind<float,float>((instance,x,y) => instance.Move(x,y));
        }

        private void _CreatePlayground(IGPIBinderFactory factory)
        {
            var gpi = factory.Create(this._User.Agent.QueryNotifier<Regulus.Game.Tennis1.Protocol.IPlayground>());
            gpi.Bind((instance) => instance.Exit());

        }

        private void _CreatePlayer(IGPIBinderFactory factory)
        {
            var gpi = factory.Create(this._User.Agent.QueryNotifier<Regulus.Game.Tennis1.Protocol.IPlayer>());
            gpi.SupplyEvent += _PlayerSupply;
            gpi.UnsupplyEvent += _PlayerUnsupply;

        }

        private void _PlayerUnsupply(IPlayer source)
        {
            source.MoveEvent -= _OnPlayerMove;
        }

        private void _PlayerSupply(IPlayer source)
        {
            source.MoveEvent += _OnPlayerMove;
        }

        private void _OnPlayerMove(Move obj)
        {
            _Viewer.WriteLine($"Start={obj.Start.ToString()} Vector={obj.Vector.ToString()} Speed={obj.Speed}");
        }

        private void _CreateMatch(IGPIBinderFactory factory)
        {
            var gpi = factory.Create(this._User.Agent.QueryNotifier<Regulus.Game.Tennis1.Protocol.IMatchable>());
            gpi.Bind((instance) => instance.Cancel());
        }

        private void _CreateLounge(IGPIBinderFactory factory)
        {
            var gpi = factory.Create(this._User.Agent.QueryNotifier<Regulus.Game.Tennis1.Protocol.IPreparer>());
            gpi.BindStatic<string,int>((instance, name,count) => GPIHelper.SignUp(instance, name,count));
        }
        
        private void _CreateConnect(IGPIBinderFactory factory)
        {
            var connect = factory.Create(this._User.ConnectProvider);
            connect.BindStatic<string, int, Regulus.Remote.Value<bool>>((gpi, ip, port) => GPIHelper.Connect(gpi,ip, port), _ConnectResult);
        }

        private void _ConnectResult(Value<bool> result)
        {
            result.OnValue += (success) =>
            {
                if (success) {
                    _Viewer.WriteLine("connect success.");
                }
                else
                {
                    _Viewer.WriteLine("connect fail.");
                }
            };
        }
    }
}