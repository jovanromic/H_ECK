﻿using H_ECK.BoardElements;
using H_ECK.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H_ECK.Pieces
{
    public abstract class Piece
    {
        public bool White { get; set; }
        public char Symbol { get; set; }

        public abstract bool ValidMove(Move move, Board board);
        public virtual void Move(Move move,Board board)
        {
            board.Fields[move.End.X][move.End.Y].Piece =
                board.Fields[move.Start.X][move.Start.Y].Piece;

            board.Fields[move.Start.X][move.Start.Y].Piece = null;
        }

        public Piece(bool white)
        {
            White = white;
        }
    }
}
