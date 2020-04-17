using H_ECK.BoardElements;
using H_ECK.GameUI;
using H_ECK.MoveValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H_ECK.GameElements
{
    public class Player
    {
        public bool White { get; set; }

        public Player(bool white)
        {
            White = white;
        }

        public string ReadInput(IGameDisplay display)
        {
            string coordinates;
            Match match, shortMatch,longMatch;
            //Ocekuje se oblik zadavanja: "e4 e6"

            do
            {
                display.DisplayMessage("Please enter your move " +
                   "in the following format: e4 e6\n");
                coordinates = Console.ReadLine().ToLower();

                Regex regex = new Regex(@"[a-h][1-8]\s[a-h][1-8]");
                Regex shortCastling = new Regex(@"0-0");
                Regex longCastling = new Regex(@"0-0-0");

                match = regex.Match(coordinates);
                shortMatch = shortCastling.Match(coordinates);
                longMatch = longCastling.Match(coordinates);

            } while (!match.Success && !shortMatch.Success && !longMatch.Success);
            
            return coordinates;
        }

        public Move MoveFromInput(IGameDisplay display)
        {
            string turn = "Black";
            if (White)
                turn = "White";

            display.DisplayMessage("\nPlayer turn: " + turn + "\n");
            char[] coordinates = ReadInput(display).ToCharArray();
            if (coordinates[0] == '0')
            {
                if(coordinates.Length == 3)
                {
                    display.DisplayMessage("Short castling.\n");
                    if(White)
                        return new ShortCastling(new Field(0, 4, null), new Field(0, 6, null));
                    else return new ShortCastling(new Field(7, 4, null), new Field(7, 6, null));
                }
                else
                {
                    display.DisplayMessage("Long castling.\n");
                    if (White)
                        return new LongCastling(new Field(0, 4, null), new Field(0, 2, null));
                    else return new LongCastling(new Field(7, 4, null), new Field(7, 2, null));
                }
            }
            else
            {
                Field start = new Field(Move.RowToIndex(coordinates[1]),
                Move.ColumnToIndex(coordinates[0]), null);

                Field end = new Field(Move.RowToIndex(coordinates[4]),
                    Move.ColumnToIndex(coordinates[3]), null);

                return new Move(start, end);
            }

        }

        public Move PerformMove(Board board, IGameDisplay display)
        {
            Move m;
            do
            {
                m = MoveFromInput(display);
            } while (!Validator.ValidMove(board, m, White,display));

            board.Fields[m.Start.X][m.Start.Y].Piece.Move(m,board);

            board.LastMove = new Move(board.Fields[m.Start.X][m.Start.Y],
                board.Fields[m.End.X][m.End.Y]);

            return m;
        }
    }
}
