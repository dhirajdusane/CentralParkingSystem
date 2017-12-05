using CentralParkingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class CPSClientClass : ICPSClient
    {
        public string GateNo { get; set; }
        private ICPSMain mainInstance;

        public CPSClientClass(int gateID,ICPSMain mainInstance)
        {
            this.mainInstance = mainInstance;
            this.GateNo = "GateNo" + gateID;
            FileLogger.Instance.TryAdd(GateNo, new FileLogger(GateNo));
        }

        public void ParseInputFile(string fileName)
        {
            string[] inputs = File.ReadAllLines(fileName);
            foreach (var item in inputs)
                ParseEntry(item);
        }

        public void ParseEntry(string inputString)
        {
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString))
                Log("Empty input.");

            string[] cols = inputString.Split('\t');

            if(cols.Length != 2)
            {
                Log("Invalid input :" + inputString);
                return;
            }

            try
            {
                bool response = mainInstance.Accept(new Request() { VehicleSize = cols[0], Operation = cols[1] });
                if (response)
                    Log("Vehicle " + cols[0] + " " + cols[1] + " successful at " + GateNo);
                else
                    Log("Vehicle " + cols[0] + " " + cols[1] + " unsuccessful at " + GateNo);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        public void Log(string message)
        {
            FileLogger.Instance[GateNo].LogMessage(message);
            Console.WriteLine(message);
        }
    }
}