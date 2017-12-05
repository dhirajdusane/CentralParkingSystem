using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem.Interfaces
{
    public interface ICPSClient
    {
        string GateNo { get; set; }
        void ParseInputFile(string fileName);
        void ParseEntry(string entry);
    }
}
