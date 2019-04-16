using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Threading;

namespace AkkaNetwrokTestSub
{
    /// <summary>
    /// test sub actor
    /// </summary>
    public class TestSubActor : ReceiveActor
    {
        #region messages

        #endregion messages

        #region public members

        public ILoggingAdapter Logger { get; } = Logging.GetLogger(Context);
        private CancellationToken CancelToken { get; }
        private int MessageCount { get; set; } = 0;

        #endregion public members

        #region constructors

        /// <summary>
        /// create a props of the actor.
        /// </summary>
        public static Props Props(CancellationToken cancelToken)
        {
            return Akka.Actor.Props.Create(() =>
                new TestSubActor(cancelToken))
                .WithRouter(FromConfig.Instance);
        }

        /// <summary>
        /// constructor
        /// </summary>
        public TestSubActor(CancellationToken cancelToken)
        {
            Logger.Debug($"created the TestSub actor. [{Self.Path.ToString()}]");

            CancelToken = cancelToken;

            Receive<string>(_ => Handle(_));

            var mainActorPath = @"akka.tcp://TestMainActorSystem@127.0.0.1:8191/user/TestMainActor";

            Context.Watch(Context.ActorSelection(mainActorPath).ResolveOne(TimeSpan.FromMinutes(10)).Result);
        }

        #endregion constructors

        private void Handle(string msg)
        {
            Logger.Debug($"received the message. [{MessageCount}][{Self.Path.ToString()}]");
            try
            {
                MessageCount++;
                if (MessageCount > 10)
                {
                    Program.TestSubActorSystem.Terminate();
                    return;
                }

                Logger.Debug("receive the string message.");
                Logger.Debug($"msg is ({msg}).");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }
    }
}
