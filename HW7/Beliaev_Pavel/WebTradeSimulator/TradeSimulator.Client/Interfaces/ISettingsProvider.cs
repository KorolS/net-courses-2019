﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Client.Misc;

namespace TradeSimulator.Client.Interfaces
{
    internal interface ISettingsProvider
    {
        GameSettings GetGameSettings();
    }
}
