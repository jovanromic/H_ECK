using H_ECK.BoardElements;
using System;

namespace H_ECK.GameElements
{
    class Game
    {
        public static Board Board { get; set; }
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
                Console.WriteLine(Board.IsFieldAttacked(Board.Fields[2][3], false));

                i = i ^ 1;
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
    }
}
