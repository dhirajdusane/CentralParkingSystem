using CentralParkingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem.CPS
{
    public class CPSBuildDirector
    {
        public ICPSMain ConstructUsing(IBuilder builder)
        {
            return builder.Build();
        }
    }

    public class TestCaseBuilder : IBuilder
    {
        public const string SmallVehicle = "SV";
        public const string MediumVehicle = "MV";
        public const string LargeVehicle = "LV";

        public ICPSMain Build()
        {
            CPSMainClass main = new CPSMainClass();
            main.Attach(new VehicleTypeHandler(SmallVehicle, ReadSettingInt(SmallVehicle), main.LogFileName));
            main.Attach(new VehicleTypeHandler(MediumVehicle, ReadSettingInt(MediumVehicle), main.LogFileName));
            main.Attach(new VehicleTypeHandler(LargeVehicle, ReadSettingInt(LargeVehicle), main.LogFileName));
            return main;
        }

        private static int ReadSettingInt(string settingName)
        {
            string value = ConfigurationManager.AppSettings[settingName];
            int count;
            int.TryParse(value, out count);
            return count;
        }
    }
}
