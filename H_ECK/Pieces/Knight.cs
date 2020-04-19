using H_ECK.BoardElements;
using H_ECK.GameElements;
using System;

namespace H_ECK.Pieces
{
    class Knight : Piece
    {
        public Knight(bool white)
            :base(white)
        {
            if (white)
                Symbol = (char)Symbols.WhiteKnight;
            else Symbol = (char)Symbols.BlackKnight;
        }

        public override bool ValidMove(Move move, Board board)
        {
            return (Math.Abs(move.End.X - move.Start.X) *
                Math.Abs(move.End.Y - move.Start.Y) == 2); 
        }
    }
}
