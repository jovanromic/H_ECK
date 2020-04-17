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
    class Rook : Piece
    {
        public bool HasMoved { get; set; }
        public Rook(bool white)
            : base(white)
        {
            if (white)
                Symbol = 'R';
            else Symbol = 'r';
            HasMoved = false;
        }

        public override void Move(Move move, Board board)
        {
            base.Move(move, board);
            HasMoved = true;
        }

        public override bool ValidMove(Move move, Board board)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            int endX = move.End.X;
            int endY = move.End.Y;
            Field inTheWay;

            if (startX != endX && startY != endY)
                return false;

            //trazenje vertikalno
            else if(startX == endX)
            {
                //grana iznad
                if (endY > startY)
                    inTheWay = BoardExplorer.ExploreNorth(board, move.Start,move.End);
                //grana ispod
                else inTheWay = BoardExplorer.ExploreSouth(board,move.Start,move.End);
            }
            //trazenje horizontalno
            else
            {
                if (endX > startX)
                    inTheWay = BoardExplorer.ExploreEast(board, move.Start,move.End);
                else inTheWay = BoardExplorer.ExploreWest(board, move.Start,move.End);
            }
            return BoardExplorer.IsPathClear(inTheWay, move.End);
        }
    }
}
