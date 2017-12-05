using CentralParkingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class VehicleTypeHandler : IVehicleTypeHandler
    {
        private string shortName;
        private int maxCount,occupiedCount;
        private readonly object lockObject;

        private const string IN = "IN";
        private const string OUT = "OUT";
        private string LogFileKeyName;

        public VehicleTypeHandler(string shortName, int maxCount,string logFile)
        {
            this.LogFileKeyName = logFile;
            this.shortName = shortName;
            this.maxCount = maxCount;
            lockObject = new object();
        }

        public bool HandleRequest(Request request)
        {
            lock (lockObject)
            {
                switch(request.Operation)
                {
                    case IN:
                        if (occupiedCount >= maxCount)
                        {
                            Log(shortName + " fully occupied!");
                            return false;
                        }
                        else
                        {
                            occupiedCount++;
                            LogRemainingSpaces(request);
                            return true;
                        }

                    case OUT:
                        if (occupiedCount <= 0)
                        {
                            Log(shortName + " was already empty!");
                            return false;
                        }
                        else
                        {
                            occupiedCount--;
                            LogRemainingSpaces(request);
                            return true;
                        }
                    default:
                        Log(string.Format("{0} {1} invalid operation. Only {2} {3} are valid operation.", 
                            request.VehicleSize, request.Operation, IN, OUT));
                        return false;
                }
            }
        }

        private void LogRemainingSpaces(Request request)
        {
            Log(request.VehicleSize + " " + request.Operation + " : Remaining space - " + (maxCount - occupiedCount));
        }

        public void Log(string message)
        {
            FileLogger.Instance[LogFileKeyName].LogMessage(message);
            Console.WriteLine(message);
        }

        public string TypeName
        {
            get { return shortName; }
        }

        public int MaxCount
        {
            get { return maxCount; }
        }
    }
}
