﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    public interface IDoorsNumbersGenerator
    {
        int[] GenerateDoorsNumbers(int doorsAmount);
    }
}
