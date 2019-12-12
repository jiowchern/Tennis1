using NUnit.Framework;
using NSubstitute;
namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }




        // 準備玩家已SignIn
        [Test]
        public void LoungeSignInTest()
        {

            var binder = new LoungeBinder();
            var user = new Regulus.Game.Tennis1.Game.User(binder);
            var lounge = new Regulus.Game.Tennis1.Game.Lounge();
            int challenge = 0;
            lounge.ChallengeEvent += (id, name) =>
            {
                challenge++;
            };
            lounge.Join(user);

            binder.Lounge.SignUp(new Regulus.Game.Tennis1.Protocol.Registration() { Name = "1" , PlayerNumber =  2 });
            binder.Lounge.SignUp(new Regulus.Game.Tennis1.Protocol.Registration() { Name = "1", PlayerNumber = 2 });

            Assert.AreEqual(true, binder.IsUnbind);

            Assert.IsTrue(challenge == 1);
        }


        // 兩個玩家匹配完成
        [Test]
        public void MatchJoinTest()
        {
            var binder1 = new MatchBinder();
            var user1 = new Regulus.Game.Tennis1.Game.User(binder1);

            var binder2 = new MatchBinder();
            var user2 = new Regulus.Game.Tennis1.Game.User(binder2);

            var matcher = new Regulus.Game.Tennis1.Game.Matcher();

            System.Guid id1 = System.Guid.NewGuid();
            System.Guid id2 = System.Guid.NewGuid();
            matcher.MatchEvent += (ids) =>
            {
                id1 = ids[0];
                id2 = ids[1];
            };
            matcher.Join(user1);
            matcher.Join(user2);

            Assert.AreEqual(user1.Id, id1);
            Assert.AreEqual(user2.Id, id2);

            Assert.AreEqual(true, binder1.IsUnbind);
            Assert.AreEqual(true, binder2.IsUnbind);


        }

        // 匹配取消測試
        [Test]
        public void MatchCancelTest()
        {
            var binder1 = new MatchBinder();
            var user1 = new Regulus.Game.Tennis1.Game.User(binder1);

            var matcher = new Regulus.Game.Tennis1.Game.Matcher();
            matcher.Join(user1);
            int cancelCount = 0;
            matcher.CancelEvent += (id) =>
            {
                cancelCount++;
            };

            binder1.Matchable.Cancel();
            binder1.Matchable.Cancel();
            Assert.AreEqual(1, cancelCount);
            Assert.AreEqual(true, binder1.IsUnbind);
        }

        // 球場流程測試
        [Test]
        public void CourtJoinTest()
        {
            var binder1 = new CourtBinder();
            var user1 = new Regulus.Game.Tennis1.Game.User(binder1);

            var binder2= new CourtBinder();
            var user2 = new Regulus.Game.Tennis1.Game.User(binder2);

            var court = new Regulus.Game.Tennis1.Game.Court();
            int doneCount = 0;
            court.DoneEvent += (ids) => {
                doneCount++;
            };
            court.Join(new Regulus.Game.Tennis1.Game.User[] {user1,user2 });
            court.Left(user1.Id);

            Assert.AreEqual(1, doneCount);

        }

        // 球場預備流程測試
        [Test]
        public void CourtPreparationTest()
        {
            var binder1 = new CourtBinder();
            var user1 = new Regulus.Game.Tennis1.Game.User(binder1);

            var binder2 = new CourtBinder();
            var user2 = new Regulus.Game.Tennis1.Game.User(binder2);

            var preparation = new Regulus.Game.Tennis1.Game.PreparationStage(new Regulus.Game.Tennis1.Game.User[] { user1, user2 }) ;
            int doneCount = 0;
            preparation.DoneOnceEvent += () => {
                doneCount++;
            };            
            var stage = preparation as Regulus.Utility.IStage;
            stage.Enter();
            binder1.Preparable.Ready();
            binder2.Preparable.Ready();

            stage.Update();

            stage.Leave();
            
            Assert.AreEqual(1, doneCount);

        }

        // 球場離開流程測試
        [Test]
        public void CourtPlayExitTest()
        {
            var binder1 = new CourtBinder();
            var user1 = new Regulus.Game.Tennis1.Game.User(binder1);

            var binder2 = new CourtBinder();
            var user2 = new Regulus.Game.Tennis1.Game.User(binder2);

            var play = new Regulus.Game.Tennis1.Game.PlayStage(new Regulus.Game.Tennis1.Game.User[] { user1, user2 });
            int doneCount = 0;
            play.DoneOnceEvent += () => {
                doneCount++;
            };
            var stage = play as Regulus.Utility.IStage;
            stage.Enter();
            binder1.Playground.Exit();
            binder2.Playground.Exit();
            stage.Update();
            stage.Leave();

            Assert.AreEqual(1 , doneCount);
        }


        // 球場同步測試
        [Test]
        public void CourtPlayMoveTest()
        {
            var binder1 = new CourtBinder();
            var user1 = new Regulus.Game.Tennis1.Game.User(binder1);

            var binder2 = new CourtBinder();
            var user2 = new Regulus.Game.Tennis1.Game.User(binder2);

            var play = new Regulus.Game.Tennis1.Game.PlayStage(new Regulus.Game.Tennis1.Game.User[] { user1, user2 });
            
            var stage = play as Regulus.Utility.IStage;
            stage.Enter();

            var moves = new System.Collections.Generic.List<Regulus.Game.Tennis1.Protocol.Move>();
            binder1.Players[0].MoveEvent += (move) => {
                moves.Add(move);
            };
            binder1.Players[1].MoveEvent += (move) => {
                moves.Add(move);
            };
            binder2.Players[0].MoveEvent += (move) => {
                moves.Add(move);
            };
            binder2.Players[1].MoveEvent += (move) => {
                moves.Add(move);
            };

            binder1.Controll.Move(new Regulus.CustomType.Vector2(1,0));
            binder2.Controll.Move(new Regulus.CustomType.Vector2(0,1));
            
            stage.Update();
            stage.Leave();

            Assert.AreEqual(1f, moves[0].Vector.X);
            Assert.AreEqual(0f, moves[0].Vector.Y);
            Assert.AreEqual(1f, moves[1].Vector.X);
            Assert.AreEqual(0f, moves[1].Vector.Y);
            Assert.AreEqual(0f, moves[2].Vector.X);
            Assert.AreEqual(1f, moves[2].Vector.Y);
            Assert.AreEqual(0f, moves[3].Vector.X);
            Assert.AreEqual(1f, moves[3].Vector.Y);
        }
    }
}