﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace DemoChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Chess.Chess chess = new Chess.Chess();
            List<string> moves;

            while (true)
            {
                moves = chess.GetAllMoves();
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                Console.WriteLine(chess.IsCheck() ? "CHECK" : "");
            
            string move = Console.ReadLine();
            if (move == "q") break;
            if (move == "")
                    move = moves[rnd.Next(moves.Count)];
            chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess.Chess chess)
        {
            string text = "   +--------------------------------+\n";
            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }
                text += "|\n";
            }
            text += "   +--------------------------------+\n";
            text += "      a b c d e f g h\n";
            return text;
        }

        static void Print(string text)
        {
            ConsoleColor oldForeColor = Console.ForegroundColor;
            foreach(char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = oldForeColor;
        }
    }
}
