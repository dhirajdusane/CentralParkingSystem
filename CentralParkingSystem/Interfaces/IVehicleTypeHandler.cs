﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem.Interfaces
{
    public interface IVehicleTypeHandler
    {
        string TypeName { get; }
        int MaxCount { get; }
        bool HandleRequest(Request request);
    }
}