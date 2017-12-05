using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class CPSMainClass
    {
        private VehicleTypeHandler smallVehicleHandler;
        private VehicleTypeHandler mediumVehicleHandler;
        private VehicleTypeHandler largeVehicleHandler;

        public const string SmallVehicle = "SV";
        public const string MediumVehicle = "MV";
        public const string LargeVehicle = "LV";
        public const string MainCPSLog = "MainCPS";
        static CPSMainClass()
        {
            Instance = new CPSMainClass();
        }

        private CPSMainClass()
        {
            FileLogger.Instance.Add(MainCPSLog, new FileLogger(MainCPSLog));
            smallVehicleHandler = new VehicleTypeHandler(SmallVehicle, ReadSettingInt(SmallVehicle), MainCPSLog);
            mediumVehicleHandler = new VehicleTypeHandler(MediumVehicle, ReadSettingInt(MediumVehicle), MainCPSLog);
            largeVehicleHandler = new VehicleTypeHandler(LargeVehicle, ReadSettingInt(LargeVehicle), MainCPSLog);
            
        }

        private int ReadSettingInt(string settingName)
        {
            string value = ConfigurationManager.AppSettings[settingName];
            int count;
            int.TryParse(value, out count);
            return count;
        }

        public static CPSMainClass Instance { get; private set; }

        public bool AcceptVehicle(Request request)
        {
            switch (request.VehicleSize.ToUpper())
            {
                case SmallVehicle:
                    return smallVehicleHandler.HandleRequest(request);
                    //break;

                case MediumVehicle:
                    return mediumVehicleHandler.HandleRequest(request);
                    //break;

                case LargeVehicle:
                    return largeVehicleHandler.HandleRequest(request);
                    //break;

                default:
                    Log(string.Format("{0} {1} invalid operation. Only {2} {3} {4} are only valid operation.",
                        request.VehicleSize, request.Operation, SmallVehicle, MediumVehicle, LargeVehicle));
                    return false;
                    //break;
            }
        }

        public void Log(string message)
        {
            FileLogger.Instance[MainCPSLog].LogMessage(message);
            Console.WriteLine(message);
        }
    }
}