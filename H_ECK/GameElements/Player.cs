using H_ECK.BoardElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H_ECK.GameElements
{
    class Player
    {
        public bool White { get; set; }

        public Player(bool white)
        {
            White = white;
        }

        public string ReadInput()
        {
            string coordinates;
            Match match;
            //Ocekuje se oblik zadavanja: "e4 e6"
            
            do
            {
                Console.WriteLine("Please enter your move " +
                   "in the following format: e4 e6\n");
                coordinates = Console.ReadLine().ToLower();
                Regex regex = new Regex(@"[a-h][1-8]\s[a-h][1-8]");
                match = regex.Match(coordinates);
            } while (!match.Success);
            
            return coordinates;
        }

        public Move MakeAMove()
        {
            string turn = "Black";
            if (White)
                turn = "White";

            Console.WriteLine("\nPlayer turn: {0}\n",turn);
            char[] coordinates = ReadInput().ToCharArray();

            Field start = new Field(Move.RowToIndex(coordinates[1]),
                Move.ColumnToIndex(coordinates[0]), null);

            Field end = new Field(Move.RowToIndex(coordinates[4]), 
                Move.ColumnToIndex(coordinates[3]), null);

            return new Move(start, end);
        }
    }
}
