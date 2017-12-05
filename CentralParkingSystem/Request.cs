using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class Request
    {
        public string Operation { get; set; }

        /// <summary>
        /// Vehicle size is require by CPS
        /// </summary>
        public string VehicleSize { get; set; }

        /*
         * It can have other parameters too for enhancements.
         */
    }
}
