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
        [Test]
        public void LoungeJoinLeftTest()
        {
            var id1 = System.Guid.NewGuid();
            var player1 = NSubstitute.Substitute.For<Regulus.Game.Tennis1.Game.Lounge.IPlayable>();
            player1.Id.Returns(id1);
            var lounge = new Regulus.Game.Tennis1.Game.Lounge();
            lounge.Join(player1);
            lounge.Left(player1.Id);
            Assert.Pass();


        }

        [Test]
        public void LoungeSafeLeftTest()
        {
            var id1 = System.Guid.NewGuid();
            var player1 = NSubstitute.Substitute.For<Regulus.Game.Tennis1.Game.Lounge.IPlayable>();
            player1.Id.Returns(id1);
            var lounge = new Regulus.Game.Tennis1.Game.Lounge();
            lounge.Left(player1.Id);
            Assert.Pass();


        }
        [Test]
        public void MatcherOneToOneJoinTest()
        {
            var id1 = System.Guid.NewGuid();
            var player1 = NSubstitute.Substitute.For<Regulus.Game.Tennis1.Game.Matcher.IContestant>();
            player1.Id.Returns(id1);

            var id2 = System.Guid.NewGuid();
            var player2 = NSubstitute.Substitute.For<Regulus.Game.Tennis1.Game.Matcher.IContestant>();
            player2.Id.Returns(id2);

            bool done = false;
            var matcher = new Regulus.Game.Tennis1.Game.Matcher();
            matcher.MatchEvent += (contestants) =>
            {
                done = contestants[0].Id == id1 && contestants[1].Id == id2;
            };

            matcher.Join(player1);
            matcher.Join(player2);
            Assert.True(done);
            
        }

        [Test]
        public void MatcherLeftTest()
        {
            var id1 = System.Guid.NewGuid();
            var player1 = NSubstitute.Substitute.For<Regulus.Game.Tennis1.Game.Matcher.IContestant>();
            player1.Id.Returns(id1);

            var matcher = new Regulus.Game.Tennis1.Game.Matcher();
            matcher.Left(player1.Id);

            Assert.Pass();
        }

        [Test]
        public void MatcherCancelTest()
        {
            var id1 = System.Guid.NewGuid();
            var player1 = NSubstitute.Substitute.For<Regulus.Game.Tennis1.Game.Matcher.IContestant>();
            player1.Id.Returns(id1);
            bool cancel = false;
            var matcher = new Regulus.Game.Tennis1.Game.Matcher();
            matcher.CancelEvent += (contestant) => {
                cancel = true;
            };
            matcher.Join(player1);
            player1.CancelOnceEvent += Raise.Event<System.Action>();

            Assert.True(cancel);
        }


    }
}