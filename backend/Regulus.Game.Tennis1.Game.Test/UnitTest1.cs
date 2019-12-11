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

            binder.Lounge.SignUp("player");
            binder.Lounge.SignUp("player");

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
            Assert.AreEqual(1 , cancelCount);
            Assert.AreEqual(true, binder1.IsUnbind);
        }
    }
}