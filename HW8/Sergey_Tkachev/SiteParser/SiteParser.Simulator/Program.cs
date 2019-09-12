﻿using SiteParser.Simulator.Dependencies;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string startPageToParse = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";
            string baseUrl = "https://en.wikipedia.org";

            var container = new Container(new SiteParserRegistry());
            var simulator = container.GetInstance<ISimulator>();

            simulator.Start(startPageToParse, baseUrl);

            Console.ReadLine();
        }
    }
}
