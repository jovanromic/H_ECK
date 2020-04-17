using H_ECK.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H_ECK.GameUI
{
    class ConsoleDisplay : IGameDisplay
    {
        public ConsoleDisplay()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.ASCII;

        }
        public void DisplayBoard(Game game)
        {
            Console.WriteLine("   ________________________________");
            for (int i = 7; i >= 0; i--)
            {
                Console.Write(" {0} ", i + 1);
                for (int j = 0; j < 8; j++)
                {
                    if (game.Board.Fields[i][j].Piece != null)
                        Console.Write("| {0} ", game.Board.Fields[i][j].Piece.Symbol);
                    else
                        Console.Write("|   ");
                }
                Console.WriteLine("|\n   |___|___|___|___|___|___|___|___|");
            }
            Console.WriteLine("     A   B   C   D   E   F   G   H\n");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
