using MOPS.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using MOPS.Events;
using MOPS.Packages;
using MOPS.Tools.Generators;

namespace MOPS
{
    class Program
    {

        static void Main(string[] args)
        {

            List<Event> eventsList = new List<Event>();
            List<Package> queue = new List<Package>();

            Server.Server server = new Server.Server(Parameters.serverBitRate);

            Parameters.CalculateServerTime();
            Parameters.CalculateTimeBetweenPackages();
            Parameters.PrintAllParameters();
            Logs.SaveServerParameters();
            for (int n = 0; n < 100; n++)
            {
                var eventGenerator = new EventGenerator();
                eventsList = eventGenerator.InitializeEventsList();
                sortList(eventsList);
                //PrintEventList(eventsList);


                Package package = null;
                double deltaTime = 0;
                bool flag = false;

                for (int i = 0; i < eventsList.Count(); i++)
                {

                    Statistic.Time = eventsList[i].Time;

                    if (flag == true)
                    {
                        deltaTime = eventsList[i].Time - eventsList[i - 1].Time;
                        Statistic.addAveragePackageInQueue(queue.Count, deltaTime);
                    }
                    flag = true;


                    if (eventsList[i].Type == EventType.Coming)
                    {
                        package = eventsList[i].CreatePackage(i);
                        Statistic.incrementRecivedPackage();

                        if (server.Busy) // serwer zajety
                        {
                            if (queue.Count() < Parameters.queueSize)  // jest miejsce w kolejce
                            {
                                package.AddToQueueTime = Statistic.Time;
                                queue.Add(package);
                                Statistic.incrementPackageInQueue();

                            }
                            else // nie ma miejsca w kolejce
                            {
                                Statistic.incrementLostPackage();
                            }

                        }
                        else // serwer wolny
                        {
                            if (queue.Count == 0) // kolejka pusta
                            {
                                server.SetBusy();
                                eventsList.Add(CreateFinishEvent(server, package));
                                sortList(eventsList);
                            }
                            else // Cos jest w kolejce
                            {
                                queue.Add(package);
                                server.SetBusy();
                                eventsList.Add(CreateFinishEvent(server, queue[0]));
                                sortList(eventsList);
                                queue[0].GetFromQueueTime = Statistic.Time;
                                Statistic.addAverageTimeinQueue(queue[0].GetFromQueueTime - queue[0].AddToQueueTime);
                                queue.RemoveAt(0);
                            }

                        }

                    }
                    else
                    {
                        if (queue.Count != 0)
                        {
                            eventsList.Add(CreateFinishEvent(server, queue[0]));
                            sortList(eventsList);
                            queue[0].GetFromQueueTime = Statistic.Time;
                            Statistic.addAverageTimeinQueue(queue[0].GetFromQueueTime - queue[0].AddToQueueTime);
                            queue.RemoveAt(0);
                        }
                        else
                        {
                            server.SetAvailable();

                        }
                    }


                }
                sortList(eventsList);

                Statistic.simulationTime = eventsList[eventsList.Count - 1].Time;

                Parameters.PrintMainParameters();
                PrintEventList(eventsList);
                Statistic.printStatistic();
                Statistic.printAveragePackageInQueue();
                Statistic.printAverageTimeInQueue();
                Statistic.printServerLoad();

                Logs.SaveEventList(eventsList);
                
                Statistic.globalList.Add(new GlobalStatistic());
                Statistic.RESETSTATISTIC();
                
                eventsList = new List<Event>();
            }
            Statistic.calculate();
            Logs.SaveStatistic();
        }


        static Event CreateFinishEvent(Server.Server server, Package package )
        {
            Event ev = new Event(package.SourceId, EventType.Finish, (float)(Statistic.Time + Parameters.serverTime + 0.001));
            return ev;
        }

        static void PrintEventList(List<Event> events)
        {
            Console.WriteLine("[EVENT LIST]");
            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine("Source ID: " + events[i].SourceId + " Time: " + events[i].Time + "Type: " + events[i].Type);
            }
        }

        static List<Event> sortList(List<Event> list)
        {
            list.Sort((x, y) => x.Time.CompareTo(y.Time));
            return list;
        }


        //na wejsciu podac: queue size
    }
}
