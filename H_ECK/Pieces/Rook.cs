using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H_ECK.BoardElements;
using H_ECK.GameElements;

namespace H_ECK.Pieces
{
    class Rook : Piece
    {
        public Rook(bool white)
            : base(white)
        {
            if (white)
                Symbol = 'R';
            else Symbol = 'r';
        }

        public override void Eat(Field field)
        {
            throw new NotImplementedException();
        }

        public override void Move(Move move)
        {
            throw new NotImplementedException();
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
                    inTheWay = board.ExploreNorth(move.Start,move.End);
                //grana ispod
                else inTheWay = board.ExploreSouth(move.Start,move.End);
            }
            //trazenje horizontalno
            else
            {
                if (endX > startX)
                    inTheWay = board.ExploreEast(move.Start,move.End);
                else inTheWay = board.ExploreWest(move.Start,move.End);
            }
            return board.IsPathClear(inTheWay, move.End);
        }
    }
}
