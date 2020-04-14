using H_ECK.GameElements;
using H_ECK.Pieces;
using System;

namespace H_ECK.BoardElements
{
    class Board
    {
        public Field[][] Fields { get; set; }

        public Board()
        {
            Fields = new Field[8][];
            for (int i = 0; i < 8; i++)
            {
                Fields[i] = new Field[8];

                for (int j = 0; j < 8; j++)
                    Fields[i][j] = new Field(i, j, null);
            }
        }

        public void Initialize()
        {
            //poredjati figure
            for(int i = 0;i<8;i++)
            {
                Fields[1][i].Piece = new Pawn(true);
                Fields[6][i].Piece = new Pawn(false);
            }

            //topovi
            Fields[0][0].Piece = new Rook(true);
            Fields[0][7].Piece = new Rook(true);
            Fields[7][0].Piece = new Rook(false);
            Fields[7][7].Piece = new Rook(false);

            //konji
            Fields[0][1].Piece = new Knight(true);
            Fields[0][6].Piece = new Knight(true);
            Fields[7][1].Piece = new Knight(false);
            Fields[7][6].Piece = new Knight(false);

            //lovci
            Fields[0][2].Piece = new Bishop(true);
            Fields[0][5].Piece = new Bishop(true);
            Fields[7][2].Piece = new Bishop(false);
            Fields[7][5].Piece = new Bishop(false);


            //kraljice
            Fields[0][3].Piece = new Queen(true);
            Fields[7][3].Piece = new Queen(false);

            //kraljevi
            Fields[0][4].Piece = new King(true);
            Fields[7][4].Piece = new King(false);

        }

        public void PerformMove(Player player)
        {
            Move m;
            do
            {
                m = player.MakeAMove();
            } while (!ValidMove(m, player.White));

            Fields[m.End.X][m.End.Y].Piece = Fields[m.Start.X][m.Start.Y].Piece;
            Fields[m.Start.X][m.Start.Y].Piece = null;
        }

        public bool ValidMove(Move m, bool white)
        {
            int x = m.Start.X;
            int y = m.Start.Y;
            if(Fields[x][y].Piece.White != white)
            {
                Console.WriteLine("\nInvalid piece selected!\n");
                return false;
            }           
            else
            {
                //bool valid = Fields[x][y].Piece.ValidMove(m);
                //if(!valid)
                //{
                //    Console.WriteLine("\nInvalid move!'n");
                //}
                //return valid;
                return true;
            }
        }
    }
}
