using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H_ECK.BoardElements;
using H_ECK.GameElements;
using H_ECK.MoveValidation;

namespace H_ECK.Pieces
{
    class Bishop : Piece
    {
        public Bishop(bool white)
            : base(white)
        {
            if (white)
                Symbol = 'B';
            else Symbol = 'b';
        }

        public override bool ValidMove(Move move, Board board)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            int endX = move.End.X;
            int endY = move.End.Y;

            //da li je ciljno polje dijagonalno u odnosu na startno
            //pomeraj po X osi mora biti jednak pomeraju po Y osi
            if (Math.Abs(endX - startX) != Math.Abs(endY - startY))
                return false;
            else
            {
                Field inTheWay;

                if (endX > startX && endY > startY)
                    inTheWay = BoardExplorer.ExploreNorthEast(board,move.Start, move.End);

                else if (endX > startX && endY < startY)
                    inTheWay = BoardExplorer.ExploreSouthEast(board, move.Start, move.End);

                else if (endX < startX && endY < startY)
                    inTheWay = BoardExplorer.ExploreSouthWest(board, move.Start, move.End);

                else inTheWay = BoardExplorer.ExploreNorthWest(board,move.Start, move.End);

                return BoardExplorer.IsPathClear(inTheWay, move.End);

            }
        }
    }
}
