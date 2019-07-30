﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class Board : IBoard
    {
        ConsoleIO ioDevice = new ConsoleIO();
        int OriginalX;
        int OriginalY;
        public int ConteinerSizeX { get; set; }
        public int ConteinerSizeY { get; set; }
        public int OrigX
        {
            get
            {
                return ioDevice.GetCursorPosition().Item1;
            }
            set
            {
                
            }
        }
        public int OrigY
        {
            get
            {
                return ioDevice.GetCursorPosition().Item2;
            }
            set
            {

            }
        }
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                ioDevice.SetCursorPosition(OriginalX + x, OriginalY + y);
                ioDevice.WriteSymb(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ioDevice.Clear();
                ioDevice.WriteOutput(e.Message);
            }
        }

        private int Horizontal;
        private int Vertical;

        public int boardSizeX
        {
            get { return Horizontal; }
            set
            {
                if (value < 2 || value > 60)
                    Horizontal = 60;
                else
                    Horizontal = value;
            }
        }

        public int boardSizeY
        {
            get { return Vertical; }
            set
            {
                if (value < 2 || value > 20)
                    Vertical = 20;
                else
                    Vertical = value;
            }
        }


        public void Draw(IBoard board)
        {
            OriginalX = OrigX;
            OriginalY = OrigY+1;
            WriteAt("+", 0, 0);
            // Draw the left side of a 5x5 rectangle, from top to bottom.
            for (int i = 1; i < ConteinerSizeY; i++)
            {
                WriteAt("|", 0, i);
            }
            WriteAt("+", 0, ConteinerSizeY);


            // Draw the bottom side, from left to right.
            for (int i = 1; i < ConteinerSizeX; i++)
            {
                WriteAt("-", i, ConteinerSizeY);   // shortcut: WriteAt("---", 1, 4)
            }
            WriteAt("+", ConteinerSizeX, ConteinerSizeY);

            // Draw the right side, from bottom to top.
            for (int i = ConteinerSizeY - 1; i >0; i--)
            {
                WriteAt("|", ConteinerSizeX, i);
            }
            WriteAt("+", ConteinerSizeX, 0);

            // Draw the top side, from right to left.
            for (int i = ConteinerSizeX - 1; i >0; i--)
            {
                WriteAt("-", i, 0);   // shortcut: WriteAt("---", 1, 4)
            }

        }
    }
    /*
    This example produces the following results:

    +---+
    |   |
    |   |
    |   |
    +---+

    All done!

    */
}

