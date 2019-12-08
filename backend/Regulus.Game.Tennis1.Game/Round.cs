using System;
using System.Collections.Generic;
using System.Linq;

namespace Regulus.Game.Tennis1.Game
{
    internal class Round
    {
        private readonly User[] _Users;
        public readonly System.Guid Id;
        volatile bool _Done;
        public IEnumerable<Guid> ContestantIds => _Users.Select(c => c.Id);
        public System.Action DoneOnceEvent;
        readonly System.Threading.Tasks.Task _Task;
        readonly Regulus.Utility.StageMachine _Machine;
        public Round(IEnumerable<User> enumerable)
        {
            _Done = false;
            Id = Guid.NewGuid();
            this._Users = enumerable.ToArray();
            _Machine = new Utility.StageMachine();
            _Task = new System.Threading.Tasks.Task(_Run);
        }
        public void Start()
        {
            _Task.Start();
        }

        private void _Run()
        {
            _Done = false;
            _ToPreparation();

            var regulator = new Regulus.Utility.AutoPowerRegulator(new Regulus.Utility.PowerRegulator());
            while (_Done == false)
            {
                _Machine.Update();
                regulator.Operate();
            }
            _Machine.Termination();
        }
        

        private void _ToPreparation()
        {
            var stage = new PreparationStage(_Users);
            stage.DoneOnceEvent += _ToPlay;
            
            _Machine.Push(stage);
        }

        private void _ToPlay()
        {
            var stage = new PlayStage(_Users);

            stage.DoneOnceEvent += _ToDone;

            _Machine.Push(stage);
        }

        private void _ToDone()
        {
            Stop();
        }
        public void Stop()
        {
            if (!_Done )
            {
                _Done = true;
                _Task.Wait();
                DoneOnceEvent();
                DoneOnceEvent = () => { };
            }
            
        }
        public bool Has(Guid id)
        {
            return _Users.Any(contestant => contestant.Id == id);
        }
    }
}