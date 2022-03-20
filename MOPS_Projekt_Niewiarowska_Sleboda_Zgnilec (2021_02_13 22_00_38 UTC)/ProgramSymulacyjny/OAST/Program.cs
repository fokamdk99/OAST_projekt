using System;
using System.Collections.Generic;
using System.Linq;
using OAST.Events;
using OAST.Packages;
using OAST.Tools;

namespace OAST
{
    class Program
    {

        static void Main(string[] args)
        {

            List<Event> eventsList = new List<Event>();
            List<Package> queue = new List<Package>();

            UserGUI();

            Server.Server server = new Server.Server(Parameters.serverBitRate);

            Parameters.CalculateServerTime();
            Parameters.CalculateTimeBetweenPackages();
            Parameters.PrintAllParameters();
            Logs.SaveServerParameters();
            for (int n = 0; n < 100; n++)
            {

                eventsList = InitializeEventsList(eventsList);
                sortList(eventsList);
                //PrintEventList(eventsList);


                Package package = null;
                float deltaTime = 0;
                bool flag = false;

                for (int i = 0; i < eventsList.Count(); i++)
                {

                    Statistic.Time = eventsList[i].time;

                    if (flag == true)
                    {
                        deltaTime = eventsList[i].time - eventsList[i - 1].time;
                        Statistic.addAveragePackageInQueue(queue.Count, deltaTime);
                    }
                    flag = true;


                    if (eventsList[i].type == "Coming")
                    {
                        package = eventsList[i].createPackage(i);
                        Statistic.incrementRecivedPackage();

                        if (server.bussy) // serwer zajety
                        {
                            if (queue.Count() < Parameters.queueSize)  // jest miejsce w kolejce
                            {
                                package.addToQueueTime = Statistic.Time;
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
                                server.setBussy();
                                eventsList.Add(CreateFinishEvent(server, package));
                                sortList(eventsList);
                            }
                            else // Cos jest w kolejce
                            {
                                queue.Add(package);
                                server.setBussy();
                                eventsList.Add(CreateFinishEvent(server, queue[0]));
                                sortList(eventsList);
                                queue[0].getFromQueueTime = Statistic.Time;
                                Statistic.addAverageTimeinQueue(queue[0].getFromQueueTime - queue[0].addToQueueTime);
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
                            queue[0].getFromQueueTime = Statistic.Time;
                            Statistic.addAverageTimeinQueue(queue[0].getFromQueueTime - queue[0].addToQueueTime);
                            queue.RemoveAt(0);
                        }
                        else
                        {
                            server.setAvailable();

                        }
                    }


                }
                sortList(eventsList);

                Statistic.simulationTime = eventsList[eventsList.Count - 1].time;

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
            Event ev = new Event(package.sourceID, "Finish", (float)(Statistic.Time + Parameters.serverTime + 0.001));
            return ev;
        }

        static void PrintEventList(List<Event> events)
        {
            Console.WriteLine("[EVENT LIST]");
            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine("Source ID: " + events[i].sourceID + " Time: " + events[i].time + "Type: " + events[i].type);
            }
        }

        static List<Event> InitializeEventsList(List<Event> events)
        {
            if (Parameters.SourceType == "CBR")
            {
                for (int s = 1; s <= Parameters.numberOfSources; s++)
                {
                    float tmp1 = (float)Math.Round(randomNumber(), 2);

                    for (int p = 0; p < Parameters.numberOfPackages; p++)
                    {
                        Event e = new Event(s, "Coming", tmp1);
                        events.Add(e);
                        tmp1 = tmp1 + Parameters.timeBetweenPackages;
                    }
                }
            }
            else
            {
                float onTime = Parameters.ONtime;
                float offTime = Parameters.OFFtime;

                float tmpPackageinOn = onTime / Parameters.timeBetweenPackages;
                int PackageinOn = (int)(tmpPackageinOn);
                int NumberOfOnState = Parameters.numberOfPackages / PackageinOn;
                int i = 0;

                for (int s = 1; s <= Parameters.numberOfSources; s++)
                {
                    float tmp1 = (float)Math.Round(randomNumber(), 2);

                    for (int p = 0; p < Parameters.numberOfPackages; p++)
                    {
                        
                        Event e = new Event(s, "Coming", tmp1);
                        events.Add(e);
                        tmp1 = tmp1 + Parameters.timeBetweenPackages;
                        i++;
                        if (i == PackageinOn)
                        {
                            tmp1 = tmp1 + offTime;
                            i = 0;
                        }
                        
                    }
                i = 0;
                }

            }
                return events;
        }

        static float randomNumber()
        {
        Random rnd = new Random();
            double rndNumber;
            if (Parameters.SourceType == "ONOFF")
            {
                rndNumber = rnd.NextDouble();
                rndNumber = rndNumber * (Parameters.ONtime+Parameters.OFFtime);
            }
            else // cbr
            {
                rndNumber = rnd.NextDouble();
                rndNumber = rndNumber * Parameters.timeBetweenPackages;
            }
            return (float)(rndNumber);
        }


        static List<Event> sortList(List<Event> list)
        {
            list.Sort((x, y) => x.time.CompareTo(y.time));
            return list;
        }


        static void UserGUI()
        {
            String command;

            Console.WriteLine("Server bit rate:");
            Parameters.serverBitRate = int.Parse(Console.ReadLine());
            Console.WriteLine("Queue size:");
            Parameters.queueSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Number of sources:");
            Parameters.numberOfSources = int.Parse(Console.ReadLine());
            Console.WriteLine("Select type of source:");
            Console.WriteLine("1-> ON/OFF type");
            Console.WriteLine("2-> CBR type");
            command = Console.ReadLine();
            
            switch (command)
            {
                case "1":
                    Console.WriteLine("peak rate: ");
                    Parameters.peakRate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Package size: ");
                    Parameters.packageSize = int.Parse(Console.ReadLine());
                    Console.WriteLine("Off time: ");
                    Parameters.OFFtime = int.Parse(Console.ReadLine());
                    Console.WriteLine("On time: ");
                    Parameters.ONtime = int.Parse(Console.ReadLine());
                    Console.WriteLine("Number of package: ");
                    Parameters.numberOfPackages = int.Parse(Console.ReadLine());
                    Statistic.packagesInSimulation = Parameters.numberOfSources * Parameters.numberOfPackages;
                    Logs.SaveONOFFInputParameters();
                    Parameters.SourceType = "ONOFF";
                    break;

                case "2":
                    Console.WriteLine("peak rate: ");
                    Parameters.peakRate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Package size: ");
                    Parameters.packageSize = int.Parse(Console.ReadLine());
                    Console.WriteLine("Number of package: ");
                    Parameters.numberOfPackages = int.Parse(Console.ReadLine());
                    Statistic.packagesInSimulation = Parameters.numberOfSources * Parameters.numberOfPackages;
                    Logs.SaveCBRInputParameters();
                    Parameters.SourceType = "CBR";
                    break;

                default:
                    Console.WriteLine("Wrong command");
                    break;

            }


        }




















    }
}
