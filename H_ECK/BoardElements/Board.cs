using H_ECK.GameElements;
using H_ECK.MoveValidation;
using H_ECK.Pieces;
using System;
using System.Collections.Generic;

namespace H_ECK.BoardElements
{
    public class Board
    {
        public Field[][] Fields { get; set; }
        public Move LastMove { get; set; }
        public Piece LastEatenPiece {get;set;}
        public Field[] CurrentKingFields { get; set; }

        public Board()
        {
            Fields = new Field[8][];
            for (int i = 0; i < 8; i++)
            {
                Fields[i] = new Field[8];

                for (int j = 0; j < 8; j++)
                    Fields[i][j] = new Field(i, j, null);
            }
            LastMove = null;
            LastEatenPiece = null;

            CurrentKingFields = new Field[2];
        }

        public Board(Board b)
        {
            Fields = new Field[8][];
            for (int i = 0; i < 8; i++)
            {
                Fields[i] = new Field[8];

                for (int j = 0; j < 8; j++)
                    Fields[i][j] = new Field(i, j, b.Fields[i][j].Piece);
            }
            LastMove = b.LastMove;
            LastEatenPiece = b.LastEatenPiece;
            CurrentKingFields = new Field[2];
            CurrentKingFields[0] = b.CurrentKingFields[0];
            CurrentKingFields[1] = b.CurrentKingFields[1];
        }

        public void Initialize()
        {
            //////poredjati figure
            //for (int i = 0; i < 8; i++)
            //{
            //    Fields[1][i].Piece = new Pawn(true);
            //    Fields[6][i].Piece = new Pawn(false);
            //}

            ////topovi
            //Fields[0][0].Piece = new Rook(true);
            //Fields[0][7].Piece = new Rook(true);
            //Fields[7][0].Piece = new Rook(false);
            //Fields[7][7].Piece = new Rook(false);

            ////konji
            //Fields[0][1].Piece = new Knight(true);
            //Fields[0][6].Piece = new Knight(true);
            //Fields[7][1].Piece = new Knight(false);
            //Fields[7][6].Piece = new Knight(false);

            ////lovci
            //Fields[0][2].Piece = new Bishop(true);
            //Fields[0][5].Piece = new Bishop(true);
            //Fields[7][2].Piece = new Bishop(false);
            //Fields[7][5].Piece = new Bishop(false);


            ////kraljice
            //Fields[0][3].Piece = new Queen(true);
            //Fields[7][3].Piece = new Queen(false);

            ////kraljevi
            //Fields[0][4].Piece = new King(true);
            //Fields[7][4].Piece = new King(false);



            Fields[0][4].Piece = new King(true);
            Fields[0][0].Piece = new Rook(true);
            Fields[0][7].Piece = new Rook(true);

            Fields[7][4].Piece = new King(false);
            Fields[7][0].Piece = new Rook(false);
            Fields[7][7].Piece = new Rook(false);

            Fields[1][3].Piece = new Pawn(false);
            Fields[4][4].Piece = new Pawn(true);
            Fields[6][3].Piece = new Pawn(false);

            CurrentKingFields[0] = new Field(Fields[0][4]);
            CurrentKingFields[1] = new Field(Fields[7][4]);
        }

    }
}
