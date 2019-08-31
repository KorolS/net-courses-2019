﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApiClient.Devices
{
    using System;

    public class InputDevice : IInputDevice
    {
        private const bool ReadKeySilently = true;

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(ReadKeySilently);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
