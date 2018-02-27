﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Chess
    {
        private const string startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public string fen { get; private set; }
        
        public Chess(string fen = startFen)
        {
            this.fen = fen;
        }

        public Chess Move(string move)
        {
            return new Chess(fen);
        }

        public char GetFigureAt(int x, int y)
        {
            return '.';
        }
    }
}
