﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    interface IDoorsNumbersGenerator
    {
            int[] GenerateDoorsNumbers(int doorsAmount);
    }
}
