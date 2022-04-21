using System;
using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.FileReader
{
    public class FileReader : IFileReader
    {
        public string Separator { get; set; }
        public int NumberOfLinks { get; set; }
        public int NumberOfDemands { get; set; }
        public string FileName { get; set; }

        private readonly ITopology _topology;

        public FileReader(ITopology topology)
        {
            _topology = topology;
            Separator = "-1";
            FileName = String.Empty;
        }

        public void ReadFile()
        {
            var fileContent = System.IO.File.ReadLines(FileName).ToList();
            int linksStatus = ReadLinks(fileContent);
            int demandsStatus = ReadDemands(fileContent);
        }
        
        private int ReadLinks(List<string> fileContent)
        {
            NumberOfLinks = ConvertValue<int>(fileContent.ElementAt(0), 0);

            List<Link> links = new();
            for (int i = 1; i < 1 + NumberOfLinks; i++)
            {
                var items = SplitString(fileContent.ElementAt(i));

                if (items.Count != 5)
                {
                    Console.WriteLine($"Invalid number of link parameters. Expected 5, got {items.Count}");
                    return 1;
                }
                
                links.Add(new Link(i,
                    ConvertValue<int>(items.ElementAt(0), i),
                    ConvertValue<int>(items.ElementAt(1), i),
                    ConvertValue<int>(items.ElementAt(2), i),
                    ConvertValue<float>(items.ElementAt(3), i),
                    ConvertValue<int>(items.ElementAt(4), i)));
            }

            _topology.Links = links;

            return 0;
        }
        
        private int ReadDemands(List<string> fileContent)
        {
            NumberOfDemands = ConvertValue<int>(fileContent.ElementAt(NumberOfLinks + 3), NumberOfLinks + 3);
            int currentLine = NumberOfLinks + 5;

            int demandId = 1;
            
            while(currentLine < fileContent.Count)
            {
                var demandData = SplitString(fileContent.ElementAt(currentLine));
                var numberOfDemandPaths = ConvertValue<int>(fileContent.ElementAt(currentLine + 1), currentLine + 1);
                var demand = new Demand(demandId, 
                    ConvertValue<int>(demandData.ElementAt(0), currentLine), 
                    ConvertValue<int>(demandData.ElementAt(1), currentLine),
                    ConvertValue<int>(demandData.ElementAt(2), currentLine),
                    numberOfDemandPaths);

                demandId += 1;
                
                for (int j = currentLine + 2; j < currentLine + 2 + numberOfDemandPaths; j++)
                {
                    var demandPathData = SplitString(fileContent.ElementAt(j));
                    var demandPathId = ConvertValue<int>(demandPathData.ElementAt(0), j);
                    demandPathData.RemoveAt(0);
                    List<int> linkIds = new();
                    foreach (var linkId in demandPathData)
                    {
                        linkIds.Add(ConvertValue<int>(linkId, j));
                    }

                    var demandPath = new DemandPath(demandPathId, linkIds);

                    demand.DemandPaths.Add(demandPath);
                }
                
                _topology.Demands.Add(demand);

                currentLine = currentLine + 3 + numberOfDemandPaths;
            }

            return 0;
        }

        private List<string> SplitString(string line)
        {
            return line.Split(" ").Where(x => !String.IsNullOrEmpty(x)).ToList();
        }

        private T ConvertValue<T>(string value, int line)
        {
            T convertedValue;
            try
            {
                convertedValue = (T) Convert.ChangeType(value, typeof(T));
            }
            catch (ArgumentNullException ex)
            {
                string message = $"Argument null in line {line}. Exception: {ex.Message}";
                Console.WriteLine(message);
                throw new ArgumentNullException(ex.Message);
            }
            catch (FormatException ex)
            {
                string message = $"Invalid argument format in line {line}. Exception: {ex.Message}";
                Console.WriteLine(message);
                throw new FormatException(ex.Message);
            }
            catch (OverflowException ex)
            {
                string message = $"Overflow exception in line {line}. Exception: {ex.Message}";
                Console.WriteLine(message);
                throw new OverflowException(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                string message = $"Invalid cast exception in line {line}. Exception: {ex.Message}";
                Console.WriteLine(message);
                throw new InvalidCastException(ex.Message);
            }

            return convertedValue;
        }
    }
}