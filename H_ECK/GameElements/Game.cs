using H_ECK.BoardElements;
using System;
using System.Collections.Generic;

namespace H_ECK.GameElements
{
    class Game
    {
        public Board Board { get; set; }
        public Player[] Players { get; set; }

        public Game()
        {
            Board = new Board();
            Players = new Player[2];
            Players[0] = new Player(true);
            Players[1] = new Player(false);
        }

        public void StartGame()
        {
            Board.Initialize();
            Print();
            int i = 0;


            while (!EndCondition())
            {
                Board.PerformMove(Players[i]);

                Print();
                AttackedFieldList(Board.Fields[4][4]);

                //i = i ^ 1;
            }

            Console.WriteLine("H-eck mate!");

        }

        public bool EndCondition()
        {
            //todo
            return false;
        }

        public void Print()
        {
            Console.WriteLine("   ________________________________");
            for (int i = 7; i >= 0; i--)
            {
                Console.Write(" {0} ", i + 1);
                for (int j = 0; j < 8; j++)
                {
                    if (Board.Fields[i][j].Piece != null)
                        Console.Write("| {0} ", Board.Fields[i][j].Piece.Symbol);
                    else
                        Console.Write("|   ");
                }
                Console.WriteLine("|\n   |___|___|___|___|___|___|___|___|");
            }
            Console.WriteLine("     A   B   C   D   E   F   G   H\n");
        }

        public void AttackedFieldList(Field f)
        {
            List<Field> attackers = new List<Field>();
            attackers.AddRange(Board.FieldAttackers(f, true));

            foreach(Field fld in attackers)
            {
                Console.WriteLine("X:{0} Y:{1} {2}\n", fld.X, fld.Y, fld.Piece.Symbol);
            }
        }
    }
}
