﻿using HW6.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Interfaces
{
    public interface ISimulation
    {
        void PerformRandomOperation(DataInteraction dataProvider, IOutputProvider outputProvider);
    }

}
