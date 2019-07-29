﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWhichCanDraw
{
    public interface IBoard
    {
        int boardSizeX { get; set; }
        int boardSizeY { get; set; }
        void WriteAt(string s, int x, int y);
        void DrawBoard(IBoard board);
    }
}
