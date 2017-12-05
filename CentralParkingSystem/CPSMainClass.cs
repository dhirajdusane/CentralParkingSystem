using CentralParkingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    /// <summary>
    /// This is main parking system class, It follows Visitor Pattern.
    /// </summary>
    public class CPSMainClass : ICPSMain
    {
        private Dictionary<string, IVehicleTypeHandler> parkingDictionary;

        public CPSMainClass()
        {
            parkingDictionary = new Dictionary<string, IVehicleTypeHandler>();
            FileLogger.Instance.TryAdd(LogFileName, new FileLogger(LogFileName));
        }

        public void Attach(IVehicleTypeHandler typeHandler)
        {
            parkingDictionary.Add(typeHandler.TypeName, typeHandler);
        }

        public bool Accept(Request request)
        {
            if (parkingDictionary.ContainsKey(request.VehicleSize))
                return parkingDictionary[request.VehicleSize].HandleRequest(request);
            else
            {
                StringBuilder builder = new StringBuilder();
                foreach (var item in parkingDictionary.Keys)
                    builder.Append(item + " ");

                FileLogger.Log(LogFileName, string.Format("{0} {1} invalid operation. Only {2} are valid operation.",
                        request.VehicleSize, request.Operation, builder.ToString()));
                return false;
            }
        }

        public string LogFileName
        {
            get { return "MainCPS"; }
        }
    }
}