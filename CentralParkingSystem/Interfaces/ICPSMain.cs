using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem.Interfaces
{
    public interface ICPSMain
    {
        string LogFileName { get; }
        void Attach(IVehicleTypeHandler typeHandler);
        bool Accept(Request request);
    }
}
