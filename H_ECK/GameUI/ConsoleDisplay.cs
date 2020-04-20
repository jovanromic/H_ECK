using H_ECK.BoardElements;
using System;
using System.Text;

namespace H_ECK.GameUI
{
    class ConsoleDisplay : IGameDisplay
    {
        public ConsoleDisplay()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "H_ECK Chess";
            Console.CursorVisible = false;
            Console.Clear();
            

        }
        public void DisplayBoard(Board board)
        {
            Console.WriteLine("   ________________________________");
            for (int i = 7; i >= 0; i--)
            {

                Console.Write(" {0} ", i + 1);

                for (int j = 0; j < 8; j++)
                {
                    if (board.Fields[i][j].Piece != null)
                        Console.Write("| {0} ", board.Fields[i][j].Piece.Symbol);
                    else
                    {
                        Console.Write(@"|   ");
                    }

                }

                Console.WriteLine("|\n   |___|___|___|___|___|___|___|___|");

            }
            Console.WriteLine("     A   B   C   D   E   F   G   H\n");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayTimeLeft(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string formattedTime = time.ToString(@"mm\:ss");

            int previousCursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(25, Console.CursorTop - 3);
            Console.Write("Time left: " + formattedTime);
            Console.SetCursorPosition(previousCursorLeft, Console.CursorTop + 3);
        }
    }
}
