using Akka.Actor;
using System.Threading;

namespace AkkaNetwrokTestSub
{
    public class Program
    {
        public static ActorSystem TestSubActorSystem { get; set; }
        public static void Main(string[] args)
        {
            TestSubActorSystem = ActorSystem.Create("TestSubActorSystem");
            var cancelToken = new CancellationTokenSource();
            var testSubActor = TestSubActorSystem.ActorOf(TestSubActor.Props(cancelToken.Token), "TestSubActor");

            TestSubActorSystem.WhenTerminated.Wait();
            //System.Threading.Thread.Sleep(10 * 1000);

            //TestSubActorSystem = ActorSystem.Create("TestMainActorSystem");
            //var cancelToken2 = new CancellationTokenSource();
            //testSubActor = TestSubActorSystem.ActorOf(TestSubActor.Props(cancelToken2.Token), "TestMainActor");

            //TestSubActorSystem.WhenTerminated.Wait();
        }
    }
}
