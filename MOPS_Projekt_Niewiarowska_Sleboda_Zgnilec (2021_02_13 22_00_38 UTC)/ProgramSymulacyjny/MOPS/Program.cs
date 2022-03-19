using MOPS.Tools;
using System.Collections.Generic;
using System.Linq;
using MOPS.Events;
using MOPS.Packages;
using MOPS.Queue;
using MOPS.Tools.Generators;

namespace MOPS
{
    class Program
    {

        static void Main(string[] args)
        {
            CustomQueue customQueue = new CustomQueue(Parameters.queueSize);

            Server.Server server = new Server.Server(Parameters.serverBitRate);

            Parameters.CalculateServerTime();
            Parameters.CalculateTimeBetweenPackages();
            Parameters.PrintAllParameters();
            Logs.SaveServerParameters();
            for (int n = 0; n < 100; n++)
            {
                var eventGenerator = new EventGenerator();
                customQueue.EventsList = eventGenerator.InitializeEventsList(Parameters.numberOfPackages, SourceType.Poisson);
                customQueue.Sort();

                Package package = null;
                double deltaTime = 0;
                bool flag = false;

                for (int i = 0; i < customQueue.EventsList.Count(); i++)
                {

                    Statistic.Time = customQueue.EventsList[i].Time;

                    if (flag == true)
                    {
                        deltaTime = customQueue.EventsList[i].Time - customQueue.EventsList[i - 1].Time;
                        Statistic.addAveragePackageInQueue(customQueue.Queue.Count, deltaTime);
                    }
                    flag = true;


                    if (customQueue.EventsList[i].Type == EventType.Coming)
                    {
                        package = customQueue.EventsList[i].CreatePackage(i);
                        Statistic.incrementRecivedPackage();

                        if (server.Busy) // serwer zajety
                        {
                            if (customQueue.Queue.Count() < Parameters.queueSize)  // jest miejsce w kolejce
                            {
                                package.AddToQueueTime = Statistic.Time;
                                customQueue.Put(package);
                                Statistic.incrementPackageInQueue();

                            }
                            else // nie ma miejsca w kolejce
                            {
                                Statistic.incrementLostPackage();
                            }

                        }
                        else // serwer wolny
                        {
                            if (customQueue.Queue.Count == 0) // kolejka pusta
                            {
                                server.SetBusy();
                                customQueue.EventsList.Add(eventGenerator.CreateFinishEvent(package, Statistic.Time, Parameters.serverTime));
                                customQueue.Sort();
                            }
                            else // Cos jest w kolejce
                            {
                                customQueue.Put(package);
                                server.SetBusy();
                                customQueue.EventsList.Add(eventGenerator.CreateFinishEvent(customQueue.Queue[0], Statistic.Time, Parameters.serverTime));
                                customQueue.Sort();
                                customQueue.Queue[0].GetFromQueueTime = Statistic.Time;
                                Statistic.addAverageTimeinQueue(customQueue.Queue[0].GetFromQueueTime - customQueue.Queue[0].AddToQueueTime);
                                customQueue.Queue.RemoveAt(0);
                            }

                        }

                    }
                    else
                    {
                        if (customQueue.Queue.Count != 0)
                        {
                            customQueue.EventsList.Add(eventGenerator.CreateFinishEvent(customQueue.Queue[0], Statistic.Time, Parameters.serverTime));
                            customQueue.Sort();
                            customQueue.Queue[0].GetFromQueueTime = Statistic.Time;
                            Statistic.addAverageTimeinQueue(customQueue.Queue[0].GetFromQueueTime - customQueue.Queue[0].AddToQueueTime);
                            customQueue.Queue.RemoveAt(0);
                        }
                        else
                        {
                            server.SetAvailable();

                        }
                    }


                }
                customQueue.Sort();

                Statistic.simulationTime = customQueue.EventsList[customQueue.EventsList.Count - 1].Time;

                Parameters.PrintMainParameters();
                Statistic.printStatistic();
                Statistic.printAveragePackageInQueue();
                Statistic.printAverageTimeInQueue();
                Statistic.printServerLoad();

                Logs.SaveEventList(customQueue.EventsList);
                
                Statistic.globalList.Add(new GlobalStatistic());
                Statistic.RESETSTATISTIC();
                
                customQueue.EventsList = new List<Event>();
            }
            Statistic.calculate();
            Logs.SaveStatistic();
        }

        //na wejsciu podac: queue size
    }
}
