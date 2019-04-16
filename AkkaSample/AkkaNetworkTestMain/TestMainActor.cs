using Akka.Actor;
using Akka.Event;
using System;
using System.IO;

namespace AkkaNetworkTestMain
{
    /// <summary>
    /// test main actor
    /// </summary>
    public class TestMainActor : ReceiveActor
    {
        #region messages

        public class StartTest
        {
        }

        public class StopTest
        {
        }

        public class SendToSub
        {
        }

        #endregion messages

        #region public members

        private ILoggingAdapter Logger { get; } = Logging.GetLogger(Context);
        private int MessageCount { get; set; } = 0;
        private int TestNo { get; }

        #endregion public members

        #region constructors

        /// <summary>
        /// create a props of the actor.
        /// </summary>
        public static Props Props(int testNo)
        {
            return Akka.Actor.Props.Create(() =>
                new TestMainActor(testNo));
        }

        /// <summary>
        /// constructor
        /// </summary>
        public TestMainActor(int testNo)
        {
            Logger.Debug("created the TestMain actor.");

            TestNo = testNo;

            Receive<StartTest>(_ => Handle(_));
            Receive<StopTest>(_ => Handle(_));
            Receive<SendToSub>(_ => Handle(_));
        }

        #endregion constructors

        #region private methods

        private void Handle(StartTest msg)
        {
            Logger.Debug("received the StartTest message.");

            Context.System.Scheduler.ScheduleTellOnce(
                TimeSpan.FromMilliseconds(1000),
                Self,
                new SendToSub(),
                Self);
        }

        private void Handle(StopTest msg)
        {
            Logger.Debug("received the StopTest message.");

            Program.TestMainActorSystem.Terminate();
        }

        private void Handle(SendToSub msg)
        {
            Logger.Debug($"received the SendToSub message. [### {MessageCount} ###]");

            var timeout = TimeSpan.FromSeconds(10);
            // var actorPath = @"akka://TestSubActorSystem/user/TestSubActor";
            var actorPath = @"akka.tcp://TestSubActorSystem@127.0.0.1:8192/user/TestSubActor";
            var actorSelection = Context.ActorSelection(actorPath);

            //if (TestNo == 1 && MessageCount > 50)
            //{
            //    Self.Tell(new StopTest());
            //    return;
            //}

            try
            {
                MessageCount++;
                Logger.Debug($"Tell to actorSelect.... [{actorPath}]");
                var str = File.ReadAllText(@".\testtest.txt");
                // var str = "TestTest";

                var connectionTask = actorSelection.ResolveOne(timeout);
                connectionTask.Wait(timeout);
                var suber = connectionTask.Result;
                suber.Tell(str);
                // suber.Tell($"[{MessageCount}] TestTest....");
                //actorSelection.Tell(str);
            }
            catch (Exception ex)
            {
                Logger.Debug($"Cannot resolve.... [{actorPath}]" + Environment.NewLine + ex.ToString());
            }

            Context.System.Scheduler.ScheduleTellOnce(
                TimeSpan.FromMilliseconds(1 * 1000),
                Self,
                new SendToSub(),
                Self);
        }

        #endregion private methods
    }
}
