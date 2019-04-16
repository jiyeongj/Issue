using Akka.Actor;
using System.Threading;

namespace AkkaNetworkTestMain
{
    class Program
    {
        public static ActorSystem TestMainActorSystem { get; set; }

        static void Main(string[] args)
        {
            TestMainActorSystem = ActorSystem.Create("TestMainActorSystem");
            var cancelToken = new CancellationTokenSource();
            var testMainActor = TestMainActorSystem.ActorOf(TestMainActor.Props(1), "TestMainActor");
            testMainActor.Tell(new TestMainActor.StartTest());

            TestMainActorSystem.WhenTerminated.Wait();
            //System.Threading.Thread.Sleep(10 * 1000);

            //TestMainActorSystem = ActorSystem.Create("TestMainActorSystem");
            //var cancelToken2 = new CancellationTokenSource();
            //testMainActor = TestMainActorSystem.ActorOf(TestMainActor.Props(2), "TestMainActor");
            //testMainActor.Tell(new TestMainActor.StartTest());

            //TestMainActorSystem.WhenTerminated.Wait();
        }
    }
}
