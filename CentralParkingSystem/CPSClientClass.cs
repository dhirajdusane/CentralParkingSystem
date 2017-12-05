using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class CPSClientClass
    {
        private string gateNo;

        public CPSClientClass(string gateNo)
        {
            this.gateNo = gateNo;
            FileLogger.Instance.Add(gateNo, new FileLogger(gateNo));
        }

        private CPSMainClass GetCPSMainInstance()
        {
            return CPSMainClass.Instance;
        }

        public void ParseInputFile(string fileName)
        {
            string[] inputs = File.ReadAllLines(fileName);
            CPSMainClass mainInstance = GetCPSMainInstance();
            foreach (var item in inputs)
            {
                string[] cols = item.Split('\t');
                try
                {
                    bool response = mainInstance.AcceptVehicle(new Request() { VehicleSize = cols[0], Operation = cols[1] });
                    if (response)
                        Log("Vehicle " + cols[0] + " " + cols[1] + " successful at " + gateNo);
                    else
                        Log("Vehicle " + cols[0] + " " + cols[1] + " unsuccessful at " + gateNo);
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                }
            }
        }

        public void Log(string message)
        {
            FileLogger.Instance[gateNo].LogMessage(message);
            Console.WriteLine(message);
        }
    }
}