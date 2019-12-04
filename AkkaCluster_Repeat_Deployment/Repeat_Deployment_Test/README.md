An exception occurs when the Non-Seed node is executed and terminated repeatedly.

This is Scenario.

1. Start application SeedNode, NonSeedNode1, NonSeedNode2.
2. Run the **`NonSeedNode2/test.bat`** for test automation.
   - This batch file repeats the process of shutting down and running NonSeedNode2.exe. Set the Timeout value between each course to 5. Each process takes place once in five seconds.
3. If this process is repeated more than 10 times, the next log is output and SeedNode is down.
```
[INFO][2019-12-04 오전 6:20:10][Thread 0016][Akka.Remote.Transport.DotNetty.TcpServerHandler] Connection was reset by the remote peer. Channel [[::ffff:127.0.0.1]:8081->[::ffff:127.0.0.1]:12267](Id=63306db5)
[DEBUG][2019-12-04 오전 6:20:10][Thread 0011][[akka://ClusterLab/system/transports/akkaprotocolmanager.tcp.0/akkaProtocol-tcp%3A%2F%2FClusterLab%40%5B%3A%3Affff%3A127.0.0.1%5D%3A12267-15#454443577]] Association between local [tcp://ClusterLab@localhost:8081] and remote [tcp://ClusterLab@[::ffff:127.0.0.1]:12267] was disassociated because the ProtocolStateActor failed: Shutdown
[DEBUG][2019-12-04 오전 6:20:10][Thread 0012][remoting] Remote system with address [akka.tcp://ClusterLab@localhost:8092] has shut down. Address is now gated for 5000ms, all messages to this address will be delivered to dead letters.

Process is terminated due to StackOverflowException.
```
4. When debugging the SeedNode where **StackOverflowException** occur, the call stack is as follows.

: Akka/Address.cs

![image](https://user-images.githubusercontent.com/45417052/70119127-f511ec80-16ac-11ea-9acb-6afc5d9e8a81.png)
